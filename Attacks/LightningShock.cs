using System;
using RRRCore;
using UnityEngine;

namespace Friendlies.Attacks
{
    public static class LightningShock
    {
        public static GameObject Get(
            GameObject owner,
            string weaponName = "Eikthyr_charge",
            string animationName = "sword_secondary"
            )
        {
            bool alreadyExisted = false;
            GameObject clone = RRRLateLoadPrefabs.CloneRepeatable(ref alreadyExisted, weaponName, "LightningShock", regOdb: true);
            if (alreadyExisted)
                return clone;

            ItemDrop component = clone.GetComponent<ItemDrop>();
            if ((UnityEngine.Object)component == (UnityEngine.Object)null)
                throw new NullReferenceException("No ItemDrop component in prefab: " + weaponName);
            ItemDrop.ItemData.SharedData shared = component.m_itemData.m_shared;

            shared.m_name = "Lightning Magic";
            shared.m_description = "A Magical Lightning Attack";
            
            shared.m_attackForce = 30f;
            shared.m_attack.m_attackAnimation = animationName;
            shared.m_damages.m_lightning = 18f;
            shared.m_damages.m_chop = 0;
            shared.m_damages.m_pickaxe = 0;
            shared.m_aiAttackRange = 7.2f;
            shared.m_aiAttackRangeMin = 0.5f;
            shared.m_aiAttackInterval = 4f;
            shared.m_skillType = Skills.SkillType.Unarmed;
            shared.m_animationState = ItemDrop.ItemData.AnimationState.Unarmed;
            shared.m_attack.m_attackOriginJoint = "RightHandMiddle2";
            shared.m_attack.m_attackRange = 7f;

            GameObject lightningClone = RRRLateLoadPrefabs.Clone("fx_eikthyr_forwardshockwave", "fx_smallerShockwave", true, false);

            foreach (Transform componentsInChild in lightningClone.GetComponentsInChildren<Transform>())
            {
                componentsInChild.localScale = new Vector3(0.025f, 0.02f, 0.08f);
            }

            shared.m_triggerEffect.m_effectPrefabs[0].m_prefab = lightningClone;
            
            return clone;
        }
    }
}
