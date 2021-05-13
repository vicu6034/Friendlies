using HarmonyLib;
using BepInEx;
using BepInEx.Logging;
using RRRCore;
using Friendlies.Attacks;
using Friendlies.Mobs;
using UnityEngine;
using System.Reflection;

namespace Friendlies
{
    [BepInPlugin("som.Friendlies", "Friendlies", "0.0.1")]
    [BepInDependency(RRRPluginGuids.RRRCore)]
    //[BepInDependency(RRRPluginGuids.RRRSpawnVariety, BepInDependency.DependencyFlags.SoftDependency)]

    public class Plugin : BaseUnityPlugin
    {
        private const string GUID = "som.Friendlies";
        private const string NAME = "Friendlies";
        private const string VERSION = "0.0.1";
        internal static ManualLogSource log;

        private void Awake()
        {
            Plugin.log = this.Logger;
            RRRLateLoadPrefabs.LateLoadPrefabs += LateLoadPrefabs;
        }

        private void LateLoadPrefabs()
        {
            if (!Util.IsGameInMainScene())
                return;
            AsheBow.Get();
            AsheBow2.Get();
            AsheBow3.Get();
            AsheBow4.Get();
            AsheKnife.Get();
            Fireball.Get(RRRLateLoadPrefabs.Clone("RRR_NPC", "NPCclone", true, true));
            AxeJump.Get(RRRLateLoadPrefabs.Clone("RRR_NPC", "NPCclone2", true, true), "KnifeCopper");
            Groot.LateLoadGroot(RRRLateLoadPrefabs.Clone(Groot.OriginalName, MobNames.Groot.ToString(), true, false));
            Ashe.LateLoadAshe(RRRLateLoadPrefabs.Clone(Ashe.OriginalName, MobNames.Ashe.ToString(), true, false));
            MiniSkelly.LateLoadMiniSkelly(RRRLateLoadPrefabs.Clone(MiniSkelly.OriginalName, MobNames.MiniSkelly.ToString(), true, false));
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyPatch(typeof(Character), "Damage")]
        private static class Character_Damaged_Patch
        {
            private static void Prefix(
              ref Character __instance,
              ref ZNetView ___m_nview,
              ref HitData hit)
            {
                string name = __instance.m_name;
                if (name == "Groot")
                {
                    System.Random rand = new System.Random();
                    int numMobs = rand.Next(1, 3);
                    GameObject prefab = ZNetScene.instance.GetPrefab("MiniSkelly");
                    for (int i = 0; i < numMobs; i++)
                    {
                        Quaternion rotation = Quaternion.Euler(0.0f, UnityEngine.Random.Range(0.0f, 360f), 0.0f);
                        Vector3 vector3 = UnityEngine.Random.insideUnitSphere * 5f;
                        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab, __instance.transform.localPosition + vector3, rotation);
                        Vector3 insideUnitSphere = UnityEngine.Random.insideUnitSphere;
                        if ((double)insideUnitSphere.y < 0.0)
                            insideUnitSphere.y = -insideUnitSphere.y;
                        gameObject.GetComponent<Rigidbody>().AddForce(insideUnitSphere * 5f, (ForceMode)2);
                        gameObject.GetComponent<Character>().SetLevel(__instance.GetLevel());
                    }
                }
                else return;
            }
        }
    }
}

