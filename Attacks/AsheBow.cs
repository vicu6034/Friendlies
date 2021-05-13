using System;
using RRRCore;
using UnityEngine;
using BepInEx;

namespace Friendlies.Attacks
{
    public static class AsheBow
    {
        public static GameObject Get(
            //string ownerName = "RRRN_Ashe",
            string weaponName = "BowHuntsman",
            string projectileName = "bow_projectile_frost"
            )
        {
            //Increase scale of bows

            bool alreadyExisted = false;
            GameObject clone = RRRLateLoadPrefabs.CloneRepeatable(ref alreadyExisted, weaponName, "AsheBow", regOdb: true);
            if (alreadyExisted)
                return clone;

            Transform transform = clone.transform;
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

            ItemDrop component = clone.GetComponent<ItemDrop>();
            if ((UnityEngine.Object)component == (UnityEngine.Object)null)
                throw new NullReferenceException("No ItemDrop component in prefab: " + weaponName);
            ItemDrop.ItemData.SharedData shared = component.m_itemData.m_shared;
            
            shared.m_name = "Ashe Bow";
            shared.m_description = "Ashe's Mighty Volley Attack";
            shared.m_useDurability = false;
            shared.m_ammoType = "";
            
            shared.m_damages.m_frost = 12f;
            shared.m_damages.m_pierce = 15f;
            
            shared.m_aiAttackRange = 40f;
            shared.m_aiAttackRangeMin = 10f;
            shared.m_aiAttackMaxAngle = 10f;
            shared.m_aiAttackInterval = 20f;

            shared.m_attackForce = 15f;
            shared.m_attack.m_projectileVel = 40f;
            shared.m_attack.m_projectileAccuracy = 75f;
            shared.m_attack.m_projectileAccuracyMin = 100f;
            shared.m_attack.m_projectiles = 3;
            shared.m_attack.m_attackProjectile = ZNetScene.instance.GetPrefab(projectileName);

            return clone;
        }
    }
 }
