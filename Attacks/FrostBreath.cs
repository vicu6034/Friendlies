using System;
using RRRCore;
using UnityEngine;

namespace Friendlies.Attacks
{
    public static class FrostBreath
    {
        public static GameObject Get(
            GameObject owner,
            string weaponName = "dragon_coldbreath",
            string animationName = "sword_secondary"
            )
        {
            bool alreadyExisted = false;
            GameObject clone = RRRLateLoadPrefabs.CloneRepeatable(ref alreadyExisted, weaponName, "FrostBreath", regOdb: true);
            if (alreadyExisted)
                return clone;

            ItemDrop component = clone.GetComponent<ItemDrop>();
            if ((UnityEngine.Object)component == (UnityEngine.Object)null)
                throw new NullReferenceException("No ItemDrop component in prefab: " + weaponName);
            ItemDrop.ItemData.SharedData shared = component.m_itemData.m_shared;

            shared.m_name = "Frost Magic";
            shared.m_description = "A Magical Frost Attack";

            shared.m_attackForce = 10f;
            shared.m_attack.m_attackAnimation = animationName;
            shared.m_damages.m_frost = 20f;
            shared.m_damages.m_chop = 0;
            shared.m_damages.m_pickaxe = 0;
            shared.m_aiAttackRange = 5.5f;
            shared.m_aiAttackRangeMin = 0.5f;
            shared.m_aiAttackInterval = 4f;
            shared.m_skillType = Skills.SkillType.FrostMagic;
            shared.m_animationState = ItemDrop.ItemData.AnimationState.Unarmed;
            shared.m_attack.m_attackOriginJoint = "RightHandMiddle3_end";
            shared.m_attack.m_attackRange = 6f;
            shared.m_attack.m_attackAngle = 45;
            shared.m_attack.m_maxYAngle = 0;

            GameObject boom = RRRLateLoadPrefabs.Clone("sfx_dragon_coldbreath_start", "sfx_dragonStartLowered", true, true);
            ZSFX zSFX = boom.GetComponent<ZSFX>();
            zSFX.m_minVol = 0.17f;
            zSFX.m_maxVol = 0.17f;
            zSFX.m_minPitch = 2f;
            zSFX.m_maxPitch = 2.1f;
            shared.m_startEffect.m_effectPrefabs[0].m_prefab = boom;

            GameObject boom2 = RRRLateLoadPrefabs.Clone("sfx_dragon_coldbreath_trailon", "sfx_dragonTrailLowered", true, true);
            ZSFX zSFX2 = boom2.GetComponent<ZSFX>();
            zSFX.m_minVol = 0.17f;
            zSFX.m_maxVol = 0.17f;
            zSFX.m_minPitch = 2f;
            zSFX.m_maxPitch = 2.1f;
            CamShaker camShake = boom2.GetComponent<CamShaker>();
            camShake.m_strength = 0;
            shared.m_trailStartEffect.m_effectPrefabs[1].m_prefab = boom2;

            GameObject frostClone = RRRLateLoadPrefabs.Clone("vfx_dragon_coldbreath", "vfx_smallerColdbreath", true, false);
            foreach (Transform componentsInChild in frostClone.GetComponentsInChildren<Transform>())
            {
                componentsInChild.localScale = new Vector3(0.11f, 0.11f, 0.2f);
            }
            foreach (ParticleSystem psInChild in frostClone.GetComponentsInChildren<ParticleSystem>())
            {
                ParticleSystem.MainModule main = psInChild.main;
                main.simulationSpeed = 1.75f;
            }
            TimedDestruction timed = frostClone.GetComponent<TimedDestruction>();
            timed.m_timeout = 1.5f;
            shared.m_trailStartEffect.m_effectPrefabs[0].m_prefab = frostClone;
           
            return clone;
        }
    }
}
