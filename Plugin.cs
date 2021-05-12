using BepInEx;
using BepInEx.Logging;
using RRRCore;
using Friendlies.Attacks;
using Friendlies.Mobs;


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
            Fireball.Get();
            AxeJump.Get(MobNames.RRRN_Dwarf.ToString(), "KnifeCopper");
            Groot.LateLoadGroot(RRRLateLoadPrefabs.Clone(Groot.OriginalName, MobNames.Groot.ToString(), true, false));
            //LoadWeapons();
            //LoadMobs();
        }
        /*
        private void LoadWeapons()
        {
            
        }

        private void LoadMobs()
        {
            Groot.LateLoadGroot(RRRLateLoadPrefabs.Clone(Groot.OriginalName, MobNames.Groot.ToString(), true, false));
            //Ashe.LateLoadAshe(RRRLateLoadPrefabs.Clone(Ashe.OriginalName, MobNames.Ashe.ToString(), true, false));
            //Dwarf.LateLoadDwarf(RRRLateLoadPrefabs.Clone(Dwarf.OriginalName, MobNames.Dwarf.ToString(), true, false));
        }
        */
    }
}

