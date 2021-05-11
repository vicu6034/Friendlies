using System;
using RRRCore;
using UnityEngine;
using BepInEx;

namespace GrootMod
{
    public static class AsheBow4
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
            GameObject gameObject = RRRLateLoadPrefabs.Clone(weaponName, "Ashe_Bow4", true, true);
            ItemDrop component = gameObject.GetComponent<ItemDrop>();
            if ((UnityEngine.Object)component == (UnityEngine.Object)null)
                throw new NullReferenceException("No ItemDrop component in prefab: " + weaponName);
            ItemDrop.ItemData.SharedData shared = component.m_itemData.m_shared;

            shared.m_name = "Ashe Bow";
            shared.m_description = "Ashe's Mighty Ultimate!";
            shared.m_useDurability = false;
            shared.m_ammoType = "";
            shared.m_attackForce = 2f;
           
            shared.m_damages.m_frost = 60f;
            shared.m_damages.m_pierce = 0;

            shared.m_aiAttackRange = 50f;
            shared.m_aiAttackRangeMin = 16f;
            shared.m_aiAttackInterval = 40f;
            //shared.m_aiAttackInterval = 8f;
            shared.m_aiAttackMaxAngle = 13f;

            shared.m_attack.m_projectileVel = 4f;

            GameObject ulty = RRRLateLoadPrefabs.Clone("bow_projectile_frost", "newFrosty", true, true);
            Projectile projectile = ulty.GetComponent<Projectile>();
            projectile.m_gravity = 0f;
            projectile.m_aoe = 1.5f;
            projectile.m_hitEffects.m_effectPrefabs[0].m_scale = true;
            projectile.m_hitEffects.m_effectPrefabs[1].m_scale = true;
            
            
            GameObject boom = RRRLateLoadPrefabs.Clone("sfx_ice_destroyed", "newIce", true, true);
            ZSFX zSFX = boom.GetComponent<ZSFX>();
            zSFX.m_minVol = 20f;
            zSFX.m_maxVol = 25f;
            zSFX.m_minPitch = 1.3f;
            zSFX.m_maxPitch = 1.5f;
            zSFX.m_useCustomReverbDistance = true;
            zSFX.m_customReverbDistance = 50f;
            projectile.m_hitEffects.m_effectPrefabs[0].m_prefab = boom;
            
            shared.m_attack.m_attackProjectile = ulty;

            Transform transform = ulty.transform;
            transform.localScale = new Vector3(3.5f, 3.5f, 2f);
            
            return gameObject;
        }
    }
}
