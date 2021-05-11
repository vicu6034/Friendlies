using BepInEx;
using BepInEx.Logging;
using RRRCore;
using Friendlies.Attacks;
using Friendlies.Mobs;


namespace Friendlies
{
    [BepInPlugin("som.Groot", "Groot", "0.0.9")]
    [BepInDependency(RRRPluginGuids.RRRCore)]
    //[BepInDependency(RRRPluginGuids.RRRSpawnVariety, BepInDependency.DependencyFlags.SoftDependency)]

    public class Plugin : BaseUnityPlugin
    {
        private const string GUID = "som.Groot";
        private const string NAME = "Groot";
        private const string VERSION = "0.0.9";
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
            LoadMobs();
        }

        private void LoadWeapons()
        {
            AsheBow.Get();
            AsheBow2.Get();
            AsheBow3.Get();
            AsheBow4.Get();
            AsheKnife.Get();
            AxeJump.Get();
        }

        private void LoadMobs()
        {
            Groot.LateLoadGroot(RRRLateLoadPrefabs.Clone(Groot.OriginalName, MobNames.Groot.ToString(), true, false));
            //Ashe.LateLoadAshe(RRRLateLoadPrefabs.Clone(Ashe.OriginalName, MobNames.Ashe.ToString(), true, false));
            //Dwarf.LateLoadDwarf(RRRLateLoadPrefabs.Clone(Dwarf.OriginalName, MobNames.Dwarf.ToString(), true, false));
        }
    }
}

