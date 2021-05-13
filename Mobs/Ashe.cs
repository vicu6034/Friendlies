//Groot
//v.05
using System.Collections;
using System.Collections.Generic;
using RRRCore;
using UnityEngine;
using RRRNpcs;

namespace Friendlies
{
    internal static class Ashe
    {
        internal static string OriginalName = "RRR_NPC";

        internal static void LateLoadAshe(GameObject clone)
        {
            Character component = (Character)clone.GetComponent<Character>();
            component.m_name = "Ashe";
            Character character1 = component;
            character1.m_health = 200;
        }
    }
}