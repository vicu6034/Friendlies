//Groot
//v.05
using System;
using RRRCore;
using RRRCore.prefabs;
using UnityEngine;
using RagnarsRokare.MobAI;

namespace Friendlies.Mobs
{
    internal static class Groot 
    {
        internal static string OriginalName = "gd_king";

        private static float ShootDmgPierce = Balance.Enemy.BasicDamage(3) * 0.75f;
        private static float ShootDmgPoison = Balance.Enemy.BasicDamage(3) * 0.25f;
        private static float MeleeDmgBlunt = Balance.Enemy.BasicDamage(3) * 1.25f;

        internal static void LateLoadGroot(GameObject clone)
        {
            Character component = (Character)clone.GetComponent<Character>();
            BaseAI component2 = (BaseAI)clone.GetComponent<BaseAI>();
            
            component.m_name = "Groot";
            Character character1 = component;
            character1.m_health = 400;
            Character character2 = component;
            character2.m_acceleration = (float)(character2.m_acceleration * 0.8);
            Character character3 = component;
            character3.m_speed = (float)(character3.m_speed * 0.8);
            Character character4 = component;
            character4.m_walkSpeed = (float)(character4.m_walkSpeed * 0.8);
            Character character5 = component;
            character5.m_runSpeed = (float)(character5.m_runSpeed * 0.7);

            Rigidbody rigidbody = clone.GetComponent<Rigidbody>();
            rigidbody.mass = 50;

            Humanoid humanoid = (Humanoid)component;
            humanoid.m_defaultItems = (GameObject[])new GameObject[2]
            {
            Groot.DesignThornAttack(),
            Groot.DesignPunch()
            };
            Character.Faction nFaction = Character.Faction.Players;
            humanoid.m_faction = nFaction;
            humanoid.m_randomSets = null;
            humanoid.m_boss = false;
            humanoid.m_bossEvent = null;
            humanoid.m_defeatSetGlobalKey = null;

            MonsterAI monsterAI = (MonsterAI)clone.GetComponent<BaseAI>();
            Pathfinding.AgentType npath = Pathfinding.AgentType.HorseSize;
            monsterAI.m_pathAgentType = npath;
            monsterAI.m_viewRange = (int)30;
            monsterAI.m_spawnMessage = "I am Groot";
            monsterAI.m_deathMessage = "Groot sad";
            monsterAI.m_enableHuntPlayer = false;

            Tameable tameable = (Tameable)clone.AddComponent<Tameable>();
            GameObject wolfObject = (GameObject)RRRLateLoadPrefabs.Clone("Wolf", "wolfClone", true, true);
            Tameable wolfTame = (Tameable)wolfObject.GetComponent<Tameable>();
            tameable.m_fedDuration = wolfTame.m_fedDuration;
            tameable.m_tamingTime = wolfTame.m_tamingTime;
            tameable.m_commandable = true;

            FootStep footStep = (FootStep)clone.GetComponent<FootStep>();
            GameObject greyObject = (GameObject)RRRLateLoadPrefabs.Clone("Greydwarf_Elite", "grayClone", true, true);
            FootStep greyStep = (FootStep)greyObject.GetComponent<FootStep>();
            footStep.m_effects = greyStep.m_effects;

            CharacterDrop characterDrop = (CharacterDrop)clone.GetComponent<CharacterDrop>();
            characterDrop.m_drops.Clear();
            CharacterDrop.Drop drop = new CharacterDrop.Drop()
            {
                m_prefab = ZNetScene.instance.GetPrefab("Wood")
            };
            characterDrop.m_drops.Add(drop);

            GameObject bruteDeath = RRRLateLoadPrefabs.Clone("sfx_greydwarf_elite_death", "vfx_" + clone.name + "_death", true, false);
            humanoid.m_deathEffects.m_effectPrefabs[0].m_prefab = bruteDeath;

            GameObject clone1 = RRRLateLoadPrefabs.Clone("Greydwarf_elite_ragdoll", clone.name + "_ragdoll", true, false);
            ((EffectList.EffectData)((EffectList)component.m_deathEffects).m_effectPrefabs[1]).m_prefab = (GameObject)clone1;
            
            Groot.DesignAppearance(clone);
  
        }

        private static GameObject DesignThornAttack()
        {
            GameObject gameObject = RRRLateLoadPrefabs.Clone("gd_king_shoot", MobNames.Groot.ToString() + "_shoot", true, true);
            ItemDrop.ItemData.SharedData shared = ((ItemDrop.ItemData)((ItemDrop)gameObject.GetComponent<ItemDrop>()).m_itemData).m_shared;
            shared.m_aiAttackRangeMin = 2.0f;
            shared.m_dodgeable = true;
            shared.m_blockable = true;
            shared.m_damages.m_poison = Groot.ShootDmgPoison;
            shared.m_damages.m_pierce = Groot.ShootDmgPierce;
            shared.m_damages.m_chop = 0;
            shared.m_damages.m_pickaxe = 0;
            shared.m_attack.m_attackAnimation = "shoot";
            shared.m_attack.m_projectileVel = (float)20.0;
            shared.m_attack.m_attackHeight = (float)1.200000011920929;
            shared.m_attack.m_attackRange = (float)2.15000009536743;
            shared.m_attack.m_attackAngle = (float)2.0;
            shared.m_attack.m_attackRayWidth = (float)1.0;
            shared.m_attack.m_projectileBursts = (int)1;
            shared.m_attack.m_projectileAccuracy = (float)1.0;
            shared.m_attack.m_burstInterval = (float)0.5;
            shared.m_aiAttackInterval = (float)7.0;
            shared.m_aiAttackRange = (float)25.0;
            shared.m_aiAttackRangeMin = (float)1.0;
            shared.m_startEffect = ObjectDB.instance.GetItemPrefab("gd_king_shoot").GetComponent<ItemDrop>().m_itemData.m_shared.m_startEffect;
            return gameObject;
        }

        private static GameObject DesignPunch()
        {
            GameObject gameObject = RRRLateLoadPrefabs.Clone("troll_punch", MobNames.Groot.ToString() + "_punch", true, true);
            ItemDrop.ItemData.SharedData shared = ((ItemDrop.ItemData)((ItemDrop)gameObject.GetComponent<ItemDrop>()).m_itemData).m_shared;
            shared.m_damages.m_blunt = Groot.MeleeDmgBlunt;
            shared.m_damages.m_chop = 0;
            shared.m_damages.m_pickaxe = 0;
            shared.m_attack.m_attackRange = 2.5f;
            shared.m_attack.m_attackAnimation = "punch";
            shared.m_aiAttackInterval = 3.0f;
            shared.m_aiAttackRange = 2.5f;
            return gameObject;
        }

        private static void DesignAppearance(GameObject clone)
        {
            Transform transform = clone.transform;
            transform.localScale = new Vector3(0.22f, 0.25f, 0.25f);
            Color black = new Color(0, 0, 0);
            foreach (ParticleSystem componentsInChild in clone.GetComponentsInChildren<ParticleSystem>())
            {
                UnityEngine.Object.Destroy(componentsInChild);
            }
            foreach (MeshRenderer componentsInChild in (MeshRenderer[])clone.GetComponentsInChildren<MeshRenderer>())
            {
                if (componentsInChild.gameObject.name == "Cube" || componentsInChild.gameObject.name == "Cube.001")
                {
                    componentsInChild.sharedMaterial.color = black;
                }
            }
            
            /*
            
            foreach (MeshRenderer meshcomponentsInChild in clone.GetComponentsInChildren<MeshRenderer>())
            {
               if (meshcomponentsInChild.name == "Cube" || meshcomponentsInChild.name == "Cube.001")
               {
                    meshcomponentsInChild.materials[0].color = black;
               }
            }
            */
            /*
            foreach (Material matComponentsInChild in clone.GetComponentsInChildren<Material>())
            {
                if (matComponentsInChild.name == "eye_red")
                    matComponentsInChild.color = black;
            }
            //
            /*
            foreach (GameObject matComponentsInChild in clone.GetComponentsInChildren<GameObject>())
            {
                if (matComponentsInChild.name == "Cube" || matComponentsInChild.name == "Cube.01")
                {
                    Object.Destroy(matComponentsInChild);
                }
            }
            /*
            foreach (ParticleSystem componentsInChild in ZNetScene.instance.GetPrefab("gd_king").GetComponentsInChildren<ParticleSystem>())
            {
                if (componentsInChild.gameObject.name == "branches")
                {
                    ParticleSystem m0 = Object.Instantiate<ParticleSystem>(componentsInChild, clone.transform.Find("Visual/Armature.001/root/spine1/spine2/spine3/l_shoulder/l_arm1/l_arm2/l_hand/l_middle1/"));
                    var shape = m0.shape;
                    ((ParticleSystem.ShapeModule)ref shape).enabled = false;
                    ParticleSystem.MainModule main = ((ParticleSystem)m0).main;
                    ((ParticleSystem.MainModule)ref main).gravityModifier = ParticleSystem.MinMaxCurve.op_Implicit(0.05f);
                    ((ParticleSystem.MainModule)ref main).maxParticles = 80;
                    ((ParticleSystem.MainModule)ref main).startLifetime = new ParticleSystem.MinMaxCurve(1.7f, 1.8f);
                    Object.Instantiate<ParticleSystem>(m0, clone.transform.Find("Visual/Armature.001/root/spine1/spine2/spine3/r_shoulder/r_arm1/r_arm2/r_hand/r_middle1/"));
                    break;
                }
            }
            */
        }
    }
}





/*
if (((Object)((Component)componentsInChild).gameObject).name == "branches")
{
    ParticleSystem m0 = Object.Instantiate<ParticleSystem>((ParticleSystem)componentsInChild, clone.transform.Find("Visual/Armature/root/spine1/spine2/spine3/l_shoulder/l_arm1/l_arm2/l_hand/l_middle1/"));
    var shape = ((ParticleSystem)m0).shape;
    ((ParticleSystem.ShapeModule).shape).enabled = false;
    ParticleSystem.MainModule main = ((ParticleSystem)m0).main;
    ((ParticleSystem.MainModule)main.gravityModifier = ParticleSystem.MinMaxCurve.op_Implicit(0.05f);
    ((ParticleSystem.MainModule)main).maxParticles = 80;
    ((ParticleSystem.MainModule)main).startLifetime = new ParticleSystem.MinMaxCurve(1.7f, 1.8f);
    Object.Instantiate<ParticleSystem>(m0, clone.transform.Find("Visual/Armature/root/spine1/spine2/spine3/r_shoulder/r_arm1/r_arm2/r_hand/r_middle1/"));
    break;
}
*/



/*
            ParticleSystem component = ZNetScene.instance.GetPrefab("gd_king").GetComponent<ParticleSystem>();
            ParticleSystem m0 = Object.Instantiate<ParticleSystem>((ParticleSystem)component, clone.transform.Find(ParticleSystem));
            if (component)
            {
                if (!component.IsNotNull())
                {
                    ParticleSystem.Destroy(component);
                }
            }
            */
//Object.Destroy(clone.GetComponent<>());