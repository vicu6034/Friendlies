using System;
using RRRCore;
using UnityEngine;

namespace Friendlies.Attacks
{
    public static class Fireball
    {
        public static GameObject Get(
            GameObject owner,
            string weaponName = "BowHuntsman",
            //string animationName = "spear_throw"
            string projectileName = "GoblinShaman_projectile_fireball"
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
            shared.m_dodgeable = true;
            shared.m_blockable = true;
            shared.m_attackForce = 2f;
            shared.m_ammoType = "";
            /*
            shared.m_attack.m_attackType = Attack.AttackType.Projectile;
            shared.m_itemType = ItemDrop.ItemData.ItemType.Bow;
            shared.m_animationState = ItemDrop.ItemData.AnimationState.Bow;
            */
            shared.m_skillType = Skills.SkillType.FireMagic;
            
            //shared.m_aiAttackRange = 40f;
            shared.m_aiAttackRangeMin = 0f;
            shared.m_aiAttackInterval = 4f;
            //shared.m_holdAnimationState = "";
            shared.m_damages.m_fire = 30f;
            //shared.m_attack.m_attackAnimation = "unarmed_punch0";
            GameObject ulty = RRRLateLoadPrefabs.Clone("bow_projectile", "newFire", true, true);
            
            Projectile projectile = ulty.GetComponent<Projectile>();
            projectile.m_gravity = 0f;
            projectile.m_aoe = 1.5f;
            
            for (int index = 0; index < ulty.transform.childCount; ++index)
            {
                GameObject.Destroy(ulty.transform.GetChild(index).gameObject);
            }
            GameObject prefab = ZNetScene.instance.GetPrefab(projectileName);
            for (int index = 0; index < prefab.transform.childCount; ++index)
            {
                GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab.transform.GetChild(index).gameObject, ulty.transform);
                gameObject.name = gameObject.name.TrimCloneTag();
            }

            shared.m_attack.m_attackProjectile = ulty;

            return clone;
        }
    }
}
