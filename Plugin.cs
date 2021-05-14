using HarmonyLib;
using BepInEx;
using BepInEx.Logging;
using RRRCore;
using Friendlies.Attacks;
using Friendlies.Mobs;
using UnityEngine;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Friendlies
{
    [BepInPlugin("som.Friendlies", "Friendlies", "0.0.1")]
    [BepInDependency(RRRPluginGuids.RRRCore)]

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
            LoadWeapons();
            Groot.LateLoadGroot(RRRLateLoadPrefabs.Clone(Groot.OriginalName, MobNames.Groot.ToString(), true, false));
            Ashe.LateLoadAshe(RRRLateLoadPrefabs.Clone(Ashe.OriginalName, MobNames.Ashe.ToString(), true, false));
            MiniSkelly.LateLoadMiniSkelly(RRRLateLoadPrefabs.Clone(MiniSkelly.OriginalName, MobNames.MiniSkelly.ToString(), true, false));
            MiniSquito.LateLoadMiniSquito(RRRLateLoadPrefabs.Clone(MiniSquito.OriginalName, MobNames.MiniSquito.ToString(), true, false));
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        private void LoadWeapons()
        {
            AsheBow.Get();
            AsheBow2.Get();
            AsheBow3.Get();
            AsheBow4.Get();
            AsheKnife.Get();
            Fireball.Get(RRRLateLoadPrefabs.Clone("RRR_NPC", "NPCclone", true, true));
            AxeJump.Get(RRRLateLoadPrefabs.Clone("RRR_NPC", "NPCclone2", true, true), "SwordIron");
            FrostMagic.Get(RRRLateLoadPrefabs.Clone("RRR_NPC", "NPCclone3", true, true));
        }

        [HarmonyPatch(typeof(Character), "Damage")]
        private static class Character_Damaged_Patch
        {
            private static void Postfix(
              ref Character __instance,
              ref ZNetView ___m_nview,
              ref HitData hit)
            {
                var name = __instance.m_name;
                if (name == "Necromancer")
                {
                    System.Random rand = new System.Random();
                    var numMobs = rand.Next(1, 3);
                    var prefab = ZNetScene.instance.GetPrefab("MiniSkelly");
                    for (var i = 0; i < numMobs; i++)
                    {
                        var rotation = Quaternion.Euler(0.0f, UnityEngine.Random.Range(0.0f, 360f), 0.0f);
                        var vector3 = UnityEngine.Random.insideUnitSphere * 6f;
                        vector3.y = -2;
                        var gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab, __instance.transform.localPosition + vector3, rotation);
                        var insideUnitSphere = UnityEngine.Random.insideUnitSphere;
                        if ((double)insideUnitSphere.y < 0.0)
                            insideUnitSphere.y = -insideUnitSphere.y;
                        gameObject.GetComponent<Rigidbody>().AddForce(insideUnitSphere * 5f, (ForceMode)2);
                        gameObject.GetComponent<Character>().SetLevel(1);
                        gameObject.GetComponent<Tameable>().Tame();
                    }
                } else if (name == "Dvorah")
                {
                    System.Random rand = new System.Random();
                    var numMobs = rand.Next(1, 3);
                    var prefab = ZNetScene.instance.GetPrefab("MiniSquito");
                    for (var i = 0; i < numMobs; i++)
                    {
                        var rotation = Quaternion.Euler(0.0f, UnityEngine.Random.Range(0.0f, 360f), 0.0f);
                        var vector3 = UnityEngine.Random.insideUnitSphere * 6f;
                        vector3.y = 0;
                        var gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab, __instance.transform.localPosition + vector3, rotation);
                        var insideUnitSphere = UnityEngine.Random.insideUnitSphere;
                        if ((double)insideUnitSphere.y < 0.0)
                            insideUnitSphere.y = -insideUnitSphere.y;
                        gameObject.GetComponent<Rigidbody>().AddForce(insideUnitSphere * 5f, (ForceMode)2);
                        gameObject.GetComponent<Character>().SetLevel(1);
                        gameObject.GetComponent<Tameable>().Tame();
                    }
                }
                else return;
            }
        }

        [HarmonyPatch(typeof(Character), "Awake")]
        private static class Character_Awake_Patch
        {
            private static void Postfix(
                ref Character __instance,
                ref ZNetView ___m_nview
                )
            {
                var name = __instance.m_name;
                if (name == "Dvorah")
                {
                    var prefab = ZNetScene.instance.GetPrefab("MiniSquito");
                    var rotation = Quaternion.Euler(0.0f, UnityEngine.Random.Range(0.0f, 360f), 0.0f);
                    var vector3 = UnityEngine.Random.insideUnitSphere * 6f;
                    vector3.y = 2f;
                    var gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab, __instance.transform.localPosition + vector3, rotation);
                    var gameObject2 = UnityEngine.Object.Instantiate<GameObject>(prefab, __instance.transform.localPosition + vector3, rotation);
                    var insideUnitSphere = UnityEngine.Random.insideUnitSphere;
                    if ((double)insideUnitSphere.y < 0.0)
                        insideUnitSphere.y = -insideUnitSphere.y;
                    gameObject.GetComponent<Rigidbody>().AddForce(insideUnitSphere * 2f, (ForceMode)2);
                    gameObject.GetComponent<Character>().SetLevel(1);
                    gameObject.GetComponent<Tameable>().Tame();
                    gameObject2.GetComponent<Rigidbody>().AddForce(insideUnitSphere * 2f, (ForceMode)2);
                    gameObject2.GetComponent<Character>().SetLevel(1);
                    gameObject2.GetComponent<Tameable>().Tame();
                } 
                else return;
            }
        }
    }
}

