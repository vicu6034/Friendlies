using RRRCore;
using UnityEngine;

namespace Friendlies.Mobs
{
    internal static class MiniSkelly
    {
        internal static string OriginalName = "Skeleton";

        private static readonly float MeleeDmgSlash = Balance.Enemy.BasicDamage(3) * 0.18f;

        internal static void LateLoadMiniSkelly(GameObject clone)
        {
            Character component = (Character)clone.GetComponent<Character>();
            BaseAI component2 = (BaseAI)clone.GetComponent<BaseAI>();

            component.m_name = "Skeleton";
            Character character1 = component;
            character1.m_health = 20;
            Character character2 = component;
            character2.m_acceleration = (float)(character2.m_acceleration * 1.75);
            Character character3 = component;
            character3.m_speed = (float)(character3.m_speed * 3.5);
            Character character4 = component;
            character4.m_walkSpeed = (float)(character4.m_walkSpeed * 2);
            Character character5 = component;
            character5.m_runSpeed = (float)(character5.m_runSpeed * 2.5);

            Rigidbody rigidbody = clone.GetComponent<Rigidbody>();
            rigidbody.mass = 0.5f;

            Humanoid humanoid = (Humanoid)component;
            Character.Faction nFaction = Character.Faction.Players;
            humanoid.m_faction = nFaction;
            humanoid.m_defaultItems = new GameObject[1]
            {
                DesignSkellySword()
            };
            
            humanoid.m_randomShield = new GameObject[1]
            {
                DesignSkellyShield()
            };
            humanoid.m_randomWeapon = null;

            MonsterAI monsterAI = (MonsterAI)clone.GetComponent<BaseAI>();
            monsterAI.m_viewRange = (int)15;
            monsterAI.m_deathMessage = "Friendly Skelly Down!";
            monsterAI.m_enableHuntPlayer = false;
            monsterAI.m_circulateWhileCharging = false;

            Tameable tameable = (Tameable)clone.AddComponent<Tameable>();
            GameObject wolfObject = (GameObject)RRRLateLoadPrefabs.Clone("Wolf", "tameClone", true, true);
            Tameable wolfTame = (Tameable)wolfObject.GetComponent<Tameable>();
            tameable.m_fedDuration = wolfTame.m_fedDuration;
            tameable.m_tamingTime = wolfTame.m_tamingTime;
            tameable.m_commandable = true;

            CharacterDrop characterDrop = (CharacterDrop)clone.GetComponent<CharacterDrop>();
            characterDrop.m_drops.Clear();
       
            MiniSkelly.DesignAppearance(clone);

        }
        private static GameObject DesignSkellySword()
        {
            GameObject gameObject = RRRLateLoadPrefabs.Clone("skeleton_sword", "skellySword", true, true);
            
            Transform transform = gameObject.transform;
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            
            ItemDrop.ItemData.SharedData shared = ((ItemDrop.ItemData)((ItemDrop)gameObject.GetComponent<ItemDrop>()).m_itemData).m_shared;
            shared.m_attackForce = 5f;
            shared.m_attack.m_speedFactor = 0.35f;
            shared.m_attack.m_speedFactorRotation = 0.35f;
            shared.m_attack.m_attackHitNoise = 20;
            shared.m_attack.m_attackStartNoise = 5;
            shared.m_attack.m_attackHeight = 0.9f;
            shared.m_attack.m_attackRange = 1.5f;
            shared.m_damages.m_slash = MiniSkelly.MeleeDmgSlash;
            shared.m_secondaryAttack = shared.m_attack;
            
            return gameObject;
        }

        private static GameObject DesignSkellyShield()
        {
            GameObject gameObject = RRRLateLoadPrefabs.Clone("ShieldWood", "skellyShield", true, true);
            
            Transform transform = gameObject.transform;
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            
            ItemDrop.ItemData.SharedData shared = ((ItemDrop.ItemData)((ItemDrop)gameObject.GetComponent<ItemDrop>()).m_itemData).m_shared;
            shared.m_blockPower = 7;
            shared.m_deflectionForce = 7;

            return gameObject;
        }

        private static void DesignAppearance(GameObject clone)
        {
            Transform transform = clone.transform;
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            
        }
    }
}




