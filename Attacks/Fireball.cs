using System;
using RRRCore;
using UnityEngine;
using BepInEx;

namespace Friendlies.Attacks
{
    public static class Fireball
    {
        public static GameObject Get(
            string ownerName = "RRRN_Mage",
            string weaponName = "BowHuntsman",
            string projectileName = "ArrowFire"
            )
        {
            bool alreadyExisted = false;
            GameObject clone = RRRLateLoadPrefabs.CloneRepeatable(ref alreadyExisted, weaponName, "Fireball", regOdb: true);
            if (alreadyExisted)
                return clone;
            /*
            Transform transform = clone.transform;
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            */
            ItemDrop component = clone.GetComponent<ItemDrop>();
            if ((UnityEngine.Object)component == (UnityEngine.Object)null)
                throw new NullReferenceException("No ItemDrop component in prefab: " + weaponName);
            ItemDrop.ItemData.SharedData shared = component.m_itemData.m_shared;

            shared.m_name = "Fireball Bow";
            shared.m_description = "A bow that shoots fireballs";
            shared.m_useDurability = false;
            shared.m_ammoType = "";
            shared.m_attackForce = 2f;

            shared.m_damages.m_fire = 30f;

            shared.m_aiAttackRange = 50f;
            shared.m_aiAttackRangeMin = 16f;
            shared.m_aiAttackInterval = 40f;
            //shared.m_aiAttackInterval = 8f;
            shared.m_aiAttackMaxAngle = 13f;
            /*
            shared.m_attack.m_projectileVel = 20f;

            GameObject ulty = RRRLateLoadPrefabs.Clone(projectileName, "newFire", true, true);
            Projectile projectile = ulty.GetComponent<Projectile>();
            projectile.m_gravity = 0f;
            projectile.m_aoe = 1.5f;
            projectile.m_hitEffects.m_effectPrefabs[0].m_scale = true;
            projectile.m_hitEffects.m_effectPrefabs[1].m_scale = true;
            */
            /*
            for (int index = 0; index < ulty.transform.childCount; ++index)
            {
                GameObject.Destroy(ulty.transform.GetChild(index).gameObject);
            }
            GameObject prefab = ZNetScene.instance.GetPrefab("Imp_fireball_projectile");
            for (int index = 0; index < prefab.transform.childCount; ++index)
            {
                GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab.transform.GetChild(index).gameObject, ulty.transform);
                gameObject.name = gameObject.name.TrimCloneTag();
            }
            */
            /*
            GameObject boom = RRRLateLoadPrefabs.Clone("sfx_ice_destroyed", "newIce", true, true);
            ZSFX zSFX = boom.GetComponent<ZSFX>();
            zSFX.m_minVol = 20f;
            zSFX.m_maxVol = 25f;
            zSFX.m_minPitch = 1.3f;
            zSFX.m_maxPitch = 1.5f;
            zSFX.m_useCustomReverbDistance = true;
            zSFX.m_customReverbDistance = 50f;
            projectile.m_hitEffects.m_effectPrefabs[0].m_prefab = boom;
            */

            /*
            Transform transformProj = ulty.transform;
            transformProj.localScale = new Vector3(3.5f, 3.5f, 2f);
            */

            //shared.m_attack.m_attackProjectile = ulty;

            return clone;
        }
    }
}
