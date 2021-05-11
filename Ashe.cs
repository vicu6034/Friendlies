//Groot
//v.05
using System.Collections;
using System.Collections.Generic;
using RRRCore;
using UnityEngine;
using RRRNpcs;

namespace GrootMod
{
    internal static class Ashe
    {
        internal static string OriginalName = "RRR_NPC";

        internal static void LateLoadAshe(GameObject clone)
        {
            /*
            for (int index = 0; index < clone.transform.childCount; ++index)
                Object.Destroy((Object)clone.transform.GetChild(index).gameObject);
            GameObject prefab = ZNetScene.instance.GetPrefab("Player");
            GameObject eyePos = (GameObject)null;
            for (int index = 0; index < prefab.transform.childCount; ++index)
            {
                GameObject gameObject = Object.Instantiate<GameObject>(prefab.transform.GetChild(index).gameObject, clone.transform);
                gameObject.name = gameObject.name.TrimCloneTag();
                if (gameObject.name == "EyePos")
                    eyePos = gameObject;
            }
            */
        }
    }
}