using System;
using RRRCore;
using UnityEngine;
using BepInEx;
using RRRCore.prefabs._0_2_0;

namespace Friendlies.Attacks
{
    public static class AsheBow2
    {
        public static GameObject Get(
            string ownerName = "RRRN_Ashe",
            string weaponName = "BowHuntsman",
            string projectileName = "bow_projectile_frost"
            )
        {
            //bool alreadyExisted = false;
            //GameObject gameObject = RRRLateLoadPrefabs.CloneRepeatable(ref alreadyExisted, weaponName, "AsheKnife", regOdb: true);
            //if (alreadyExisted)
            //    return gameObject;
            GameObject gameObject = RRRLateLoadPrefabs.Clone(weaponName, "Ashe_Bow2", true, true);
            ItemDrop component = gameObject.GetComponent<ItemDrop>();
            if ((UnityEngine.Object)component == (UnityEngine.Object)null)
                throw new NullReferenceException("No ItemDrop component in prefab: " + weaponName);
            ItemDrop.ItemData.SharedData shared = component.m_itemData.m_shared;

            shared.m_name = "Ashe Bow";
            shared.m_description = "Ashe's Mighty Basic Attack";
            shared.m_useDurability = false;
            shared.m_ammoType = "";

            shared.m_damages.m_frost = 9;
            shared.m_damages.m_pierce = 14;
            
            shared.m_aiAttackRange = 35f;
            shared.m_aiAttackRangeMin = 5f;
            shared.m_aiAttackInterval = 1.1f;
            shared.m_aiAttackMaxAngle = 15;
            
            shared.m_attack.m_attackProjectile = ZNetScene.instance.GetPrefab(projectileName);

            return gameObject;
        }
    }
}
