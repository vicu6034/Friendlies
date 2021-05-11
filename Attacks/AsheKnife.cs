using System;
using RRRCore;
using UnityEngine;

namespace Friendlies.Attacks
{
    public static class AsheKnife
    {
        public static GameObject Get(
            string ownerName = "RRRN_Ashe"
            )
        {
            bool alreadyExisted = false;
            GameObject clone = RRRLateLoadPrefabs.CloneRepeatable(ref alreadyExisted, "KnifeChitin", "AsheKnife", regOdb: true);
            if (alreadyExisted)
                return clone;
            
            ItemDrop component = clone.GetComponent<ItemDrop>();
            if ((UnityEngine.Object)component == (UnityEngine.Object)null)
                throw new NullReferenceException("No ItemDrop component in prefab: KnifeChitin");
            ItemDrop.ItemData.SharedData shared = component.m_itemData.m_shared;

            shared.m_name = "Ashe Knife";
            shared.m_description = "Ashe's Mighty Knife";
            shared.m_useDurability = false;

            shared.m_damages.m_frost = 5;
            shared.m_damages.m_pierce = 4;
            shared.m_damages.m_slash = 4;

            shared.m_aiAttackRangeMin = 0f;
            shared.m_aiAttackRange = 1.5f;
            shared.m_aiAttackInterval = 0.8f;

            /*
            GameObject freezingVFX = RRRLateLoadPrefabs.Clone("vfx_Freezing", "newFreeze", true, true);
            //GameObject arrowSFX = RRRLateLoadPrefabs.Clone("sfx_arrow_hit", "newHit", true, true);
            //ZSFX zSFX = arrowSFX.GetComponent<ZSFX>();
            //EffectList effectDatas = new EffectList(); // RRRLateLoadPrefabs.Clone("skeleton_sword", "newEffects", true, true).GetComponent<EffectList>();
            //effectDatas.m_effectPrefabs[0].m_prefab = freezingVFX;
            //effectDatas.m_effectPrefabs[1].m_prefab = arrowSFX;
            shared.m_hitEffect.m_effectPrefabs[0].m_prefab = freezingVFX;
            */
            return clone;
        }
    }
}
