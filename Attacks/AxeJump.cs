using System;
using RRRCore;
using UnityEngine;
using BepInEx;

namespace Friendlies.Attacks
{
    public static class AxeJump
    {
        public static GameObject Get(
            string ownerName = "RRRN_Dwarf",
            string weaponName = "KnifeBlackMetal"
            )
        {
            //bool alreadyExisted = false;
            //GameObject gameObject = RRRLateLoadPrefabs.CloneRepeatable(ref alreadyExisted, weaponName, "AsheKnife", regOdb: true);
            //if (alreadyExisted)
            //    return gameObject;
            GameObject clone = RRRLateLoadPrefabs.Clone(weaponName, "AxeJump", true, true);
            ItemDrop component = clone.GetComponent<ItemDrop>();
            if ((UnityEngine.Object)component == (UnityEngine.Object)null)
                throw new NullReferenceException("No ItemDrop component in prefab: " + weaponName);
            
            for (int index = 0; index < clone.transform.childCount; ++index)
            {
                GameObject.Destroy(clone.transform.GetChild(index).gameObject);
            }
            GameObject prefab = ZNetScene.instance.GetPrefab("AxeBronze");
            for (int index = 0; index < prefab.transform.childCount; ++index)
            {
                GameObject gameObject = GameObject.Instantiate<GameObject>(prefab.transform.GetChild(index).gameObject, clone.transform);
                gameObject.name = gameObject.name.TrimCloneTag();
            }

            ZSyncTransform zSync = clone.GetComponent<ZSyncTransform>();
            zSync = prefab.GetComponent<ZSyncTransform>();
            ZNetView zNet = clone.GetComponent<ZNetView>();
            zNet = prefab.GetComponent<ZNetView>();
            Rigidbody rigidbody = clone.GetComponent<Rigidbody>();
            rigidbody = prefab.GetComponent<Rigidbody>();

            ItemDrop.ItemData.SharedData shared = component.m_itemData.m_shared;

            shared.m_name = "Axe Jump";
            shared.m_description = "jump with an axe";
            shared.m_useDurability = false;
            shared.m_skillType = Skills.SkillType.Axes;
            shared.m_animationState = ItemDrop.ItemData.AnimationState.OneHanded;
        
            shared.m_attackForce = 50;
            shared.m_backstabBonus = 3;

            shared.m_damages.m_pierce = 0;
            shared.m_damages.m_slash = 30;
            shared.m_damages.m_chop = 40;

            shared.m_aiAttackRange = 5f;
            shared.m_aiAttackRangeMin = 0f;
            shared.m_aiAttackInterval = 4f;

            shared.m_attack = shared.m_secondaryAttack;

            return clone;
        }
    }
}
