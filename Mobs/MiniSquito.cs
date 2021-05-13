using RRRCore;
using UnityEngine;


namespace Friendlies.Mobs
{
    internal static class MiniSquito
    {
        internal static string OriginalName = "Deathsquito";

        private static float MeleeDmgPierce= Balance.Enemy.BasicDamage(3) * 0.15f;

        internal static void LateLoadMiniSquito(GameObject clone)
        {
            Character component = (Character)clone.GetComponent<Character>();
            BaseAI component2 = (BaseAI)clone.GetComponent<BaseAI>();

            component.m_name = "Squito";
            Character character1 = component;
            character1.m_health = 5;
            Character character2 = component;
            character2.m_acceleration = (float)(character2.m_acceleration * 0.75);
            Character character3 = component;
            character3.m_speed = (float)(character3.m_speed * 0.75);
            Character character4 = component;
            character4.m_walkSpeed = (float)(character4.m_walkSpeed * 0.75);
            Character character5 = component;
            character5.m_runSpeed = (float)(character5.m_runSpeed * 0.75);
            Character character6 = component;
            character6.m_flySlowSpeed = (float)(character6.m_flySlowSpeed * 0.75);
            Character character7 = component;
            character7.m_flyFastSpeed = (float)(character7.m_flyFastSpeed * 1.25);

            Rigidbody rigidbody = clone.GetComponent<Rigidbody>();
            rigidbody.mass = 3f;

            Humanoid humanoid = (Humanoid)component;
            Character.Faction nFaction = Character.Faction.Players;
            humanoid.m_faction = nFaction;
            humanoid.m_defaultItems = new GameObject[1]
            {
                DesignSquitoSting()
            };


            MonsterAI monsterAI = (MonsterAI)clone.GetComponent<BaseAI>();
            monsterAI.m_viewRange = (int)15;
            monsterAI.m_deathMessage = "Friendly Squito Down!";
            monsterAI.m_enableHuntPlayer = false;
            monsterAI.m_circulateWhileCharging = false;
            monsterAI.m_flyAltitudeMin = 0.4f;

            Tameable tameable = (Tameable)clone.AddComponent<Tameable>();
            GameObject wolfObject = (GameObject)RRRLateLoadPrefabs.Clone("Wolf", "tameClone2", true, true);
            Tameable wolfTame = (Tameable)wolfObject.GetComponent<Tameable>();
            tameable.m_fedDuration = wolfTame.m_fedDuration;
            tameable.m_tamingTime = wolfTame.m_tamingTime;
            tameable.m_commandable = true;
            tameable.m_tamedEffect = new EffectList();

            CharacterDrop characterDrop = (CharacterDrop)clone.GetComponent<CharacterDrop>();
            characterDrop.m_drops.Clear();

            MiniSquito.DesignAppearance(clone);

        }

        private static GameObject DesignSquitoSting()
        {
            GameObject gameObject = RRRLateLoadPrefabs.Clone("Deathsquito_sting", "miniSting", true, true);

            ItemDrop.ItemData.SharedData shared =
                ((ItemDrop.ItemData) ((ItemDrop) gameObject.GetComponent<ItemDrop>()).m_itemData).m_shared;
            shared.m_damages.m_pierce = MiniSquito.MeleeDmgPierce;
            shared.m_secondaryAttack = shared.m_attack;

            return gameObject;
        }

        private static void DesignAppearance(GameObject clone)
        {
            Transform transform = clone.transform;
            transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);

        }
    }
}




