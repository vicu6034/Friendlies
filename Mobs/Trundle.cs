//Groot
//v.05
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RRRCore;
using UnityEngine;
using Object = System.Object;

namespace Friendlies.Mobs
{
    internal static class Trundle
    {
        internal static string OriginalName = "Troll";

        internal static void LateLoadTrundle(GameObject clone)
        {
            Character component = (Character)clone.GetComponent<Character>();
            component.m_name = "Trundle";
            Character character1 = component;
            character1.m_health = 400;
            Character character2 = component;
            character2.m_acceleration = (float)(character2.m_acceleration * 1);
            Character character3 = component;
            character3.m_speed = (float)(character3.m_speed * 1);
            Character character4 = component;
            character4.m_walkSpeed = (float)(character4.m_walkSpeed * 1);
            Character character5 = component;
            character5.m_runSpeed = (float)(character5.m_runSpeed * 1);

            //Humanoid
            Humanoid humanoid = clone.GetComponent<Humanoid>();
            humanoid.m_defaultItems = (GameObject[])new GameObject[2]
            {
            Trundle.DesignSmackH(),
            Trundle.DesignSmackV()
            };

            Character.Faction nFaction = Character.Faction.Players;
            humanoid.m_faction = nFaction;
            humanoid.m_randomSets = null;
            humanoid.m_boss = false;
            humanoid.m_bossEvent = null;
            humanoid.m_defeatSetGlobalKey = null;

            CapsuleCollider capsule = new CapsuleCollider();
            GameObject log = ZNetScene.instance.GetPrefab("FirTree_log");
            capsule = log.GetComponentInChildren<CapsuleCollider>();

            //Item Tint
            Color clubColor = new Color(0f, 0.42f, 0.95f);
            HashSet<GameObject> source = new HashSet<GameObject>();
            source.UnionWith(humanoid.m_defaultItems);
            foreach (Renderer renderer in source.Where<GameObject>(i => i.IsNotNull()).SelectMany(i => i.GetComponentsInChildren<Renderer>(true)))
            {
                foreach (Material material in renderer.materials)
                    material.SetColor("_Color", clubColor);
            }
            foreach (Transform transform in source.Where<GameObject>(i => i.IsNotNull()).SelectMany(i => i.GetComponentsInChildren<Transform>(true)))
            {
                if (transform.name == "collider")
                {
                    transform.gameObject.AddComponent<CapsuleCollider>();
                    CapsuleCollider toChange = transform.GetComponent<CapsuleCollider>();
                    toChange.center = capsule.center;
                    toChange.height = capsule.height;
                    toChange.radius = capsule.radius;
                    toChange.direction = capsule.direction;
                }
            }
            //VisEquipFix
            VisEquipment vis = component.GetComponent<VisEquipment>();
            foreach (Transform componentsInChild in component.GetComponentsInChildren<Transform>())
            {
                if (componentsInChild.name == "RightHandMiddle1")
                {
                    //componentsInChild.localPosition = new Vector3(-0.000421158f, 0.004914436f,-0.01f);
                    vis.m_rightHand = componentsInChild;
                }
            }
            
            //MonsterAI
            MonsterAI monsterAI = clone.GetComponent<MonsterAI>();
            monsterAI.m_viewRange = 25f;
            monsterAI.m_spawnMessage = "I am Trundle";
            monsterAI.m_deathMessage = "Trundle bye bye";
            monsterAI.m_enableHuntPlayer = false;
            monsterAI.m_circulateWhileCharging = true;
            monsterAI.m_circleTargetDistance = 10f;
            monsterAI.m_circleTargetInterval = 4f;
            monsterAI.m_circleTargetDuration = 2.5f;
            monsterAI.m_fleeIfHurtWhenTargetCantBeReached = true;
            monsterAI.m_alertRange = 20f;
            monsterAI.m_randomMoveInterval = 10f;
            monsterAI.m_randomMoveRange = 4f;
            //Consume
            monsterAI.m_consumeHeal = 50f;
            monsterAI.m_consumeRange = 1f;
            monsterAI.m_consumeSearchInterval = 5f;
            monsterAI.m_consumeSearchRange = 8f;
            List<ItemDrop> consumeList = new List<ItemDrop>();
            ItemDrop consumeDrop = ZNetScene.instance.GetPrefab("CookedMeat").GetComponent<ItemDrop>();
            consumeList.Add(consumeDrop);
            monsterAI.m_consumeItems = consumeList;
            //Tameable
            Tameable tameable = clone.AddComponent<Tameable>();
            GameObject wolfObject = RRRLateLoadPrefabs.Clone("Wolf", "wolfCloneforTrundle", true, true);
            Tameable wolfTame = wolfObject.GetComponent<Tameable>();
            tameable.m_fedDuration = wolfTame.m_fedDuration;
            tameable.m_tamingTime = wolfTame.m_tamingTime;
            tameable.m_commandable = true;
            tameable.m_tamedEffect = new EffectList();
            //Footstep
            FootStep footStep = clone.GetComponent<FootStep>();
            GameObject greyObject = RRRLateLoadPrefabs.Clone("Greydwarf_Elite", "grayCloneforTrundle", true, true);
            FootStep greyStep = greyObject.GetComponent<FootStep>();
            footStep.m_effects = greyStep.m_effects;

            //Death drops
            CharacterDrop characterDrop = (CharacterDrop)clone.GetComponent<CharacterDrop>();
            characterDrop.m_drops.Clear();
            /*
            CharacterDrop.Drop drop = new CharacterDrop.Drop()
            {
                m_prefab = ZNetScene.instance.GetPrefab("Wood")
            };
            characterDrop.m_drops.Add(drop);
            */

            
            /*
            HashSet<GameObject> sourceH = new HashSet<GameObject>();
            sourceH.UnionWith(hairs);
            foreach (SkinnedMeshRenderer rendererS in sourceH.Where<GameObject>((Func<GameObject, bool>)(i => i.IsNotNull())).SelectMany<GameObject, SkinnedMeshRenderer>((Func<GameObject, IEnumerable<SkinnedMeshRenderer>>)(i => (IEnumerable<SkinnedMeshRenderer>)i.GetComponentsInChildren<SkinnedMeshRenderer>(true))))
            {
                foreach (Material materialM in rendererS.materials)
                {
                    materialM.color = Color.black;
                }
            }
            */
            
            Trundle.DesignAppearance(clone);
        }

        private static GameObject DesignSmackH()
        {
            GameObject gameObject = RRRLateLoadPrefabs.Clone("Club", MobNames.Trundle.ToString() + "_smack_h", true, true);
            //Transform transform = gameObject.GetComponent<Transform>();
            //transform.localScale = new Vector3(50f, 50f, 50f);
            ItemDrop.ItemData.SharedData shared = ((ItemDrop.ItemData)((ItemDrop)gameObject.GetComponent<ItemDrop>()).m_itemData).m_shared;
            shared.m_helmetHideHair = false;
            shared.m_animationState = ItemDrop.ItemData.AnimationState.OneHanded;
            shared.m_toolTier = 2;
            shared.m_aiAttackRangeMin = 0;
            shared.m_aiAttackRange = 8f;
            shared.m_aiAttackInterval = 6f;
            shared.m_aiAttackMaxAngle = 30f;
            shared.m_aiWhenWalking = true;
            shared.m_aiTargetType = ItemDrop.ItemData.AiTarget.Enemy;
            shared.m_attackForce = 60f;
            shared.m_damages.m_blunt = 25f;
            shared.m_attack.m_speedFactor = 0;
            shared.m_attack.m_speedFactorRotation = 1;
            shared.m_attack.m_lowerDamagePerHit = false;
            shared.m_attack.m_hitThroughWalls = true;
            shared.m_attack.m_hitPointtype = Attack.HitPointType.Closest;
            shared.m_attack.m_attackAnimation = "swing_logh";
            shared.m_attack.m_attackType = Attack.AttackType.Horizontal;
            shared.m_attack.m_attackRandomAnimations = 0;
            shared.m_attack.m_attackChainLevels = 1;
            shared.m_attack.m_attackRange = 10f;
            shared.m_attack.m_attackHeight = 3f;
            shared.m_attack.m_attackOffset = 0;
            shared.m_attack.m_attackAngle = 50f;
            shared.m_attack.m_attackRayWidth = 2.3f;

            //shared.m_attack = RRRLateLoadPrefabs.Clone("troll_log_swing_h", MobNames.Trundle.ToString() + "_smack_h_util", true, true).GetComponent<Attack>();
            foreach (Transform componentsInChild in gameObject.GetComponentsInChildren<Transform>())
            {
                componentsInChild.localScale = new Vector3(2.2f, 2.5f, 2.2f);
                componentsInChild.localRotation = new Quaternion(-90, 0f, -90, 0);
                componentsInChild.localPosition = new Vector3(0f, -0.05f, -0.08f);
            }
            return gameObject;
        }

        private static GameObject DesignSmackV()
        {
            GameObject gameObject = RRRLateLoadPrefabs.Clone("Club", MobNames.Trundle.ToString() + "_smack_v", true, true);
            ItemDrop.ItemData.SharedData shared = ((ItemDrop.ItemData)((ItemDrop)gameObject.GetComponent<ItemDrop>()).m_itemData).m_shared;
            shared.m_helmetHideHair = false;
            shared.m_animationState = ItemDrop.ItemData.AnimationState.OneHanded;
            shared.m_toolTier = 2;
            shared.m_aiAttackRangeMin = 0;
            shared.m_aiAttackRange = 8f;
            shared.m_aiAttackInterval = 6f;
            shared.m_aiAttackMaxAngle = 10f;
            shared.m_aiWhenWalking = true;
            shared.m_aiTargetType = ItemDrop.ItemData.AiTarget.Enemy;
            shared.m_attackForce = 60f;
            shared.m_damages.m_blunt = 25f;
            //shared.m_attack = RRRLateLoadPrefabs.Clone("troll_log_swing_v", MobNames.Trundle.ToString() + "_smack_v_util", true, true).GetComponent<Attack>();
            shared.m_attack.m_speedFactor = 0;
            shared.m_attack.m_speedFactorRotation = 1;
            shared.m_attack.m_lowerDamagePerHit = false;
            shared.m_attack.m_hitThroughWalls = true;
            shared.m_attack.m_hitPointtype = Attack.HitPointType.Closest;
            shared.m_attack.m_attackAnimation = "swing_logv";
            shared.m_attack.m_attackType = Attack.AttackType.Vertical;
            shared.m_attack.m_attackRandomAnimations = 0;
            shared.m_attack.m_attackChainLevels = 1;
            shared.m_attack.m_attackRange = 9f;
            shared.m_attack.m_attackHeight = 3.66f;
            shared.m_attack.m_attackOffset = 0;
            shared.m_attack.m_attackAngle = 90f;
            shared.m_attack.m_attackRayWidth = 2f;
            shared.m_attack.m_maxYAngle = 0;
            
            foreach (Transform componentsInChild in gameObject.GetComponentsInChildren<Transform>())
            {
                componentsInChild.localScale = new Vector3(2.2f,2.5f,2.2f);
                componentsInChild.localRotation = new Quaternion(-90, 0f, -90, 0);
                componentsInChild.localPosition = new Vector3(0f, -0.05f, -0.08f);
            }

            return gameObject;
        }

        private static void DesignAppearance(GameObject clone)
        {
            Transform transform = clone.transform;
            transform.localScale = new Vector3(0.65f, 1.1f, 0.75f);

            foreach (SkinnedMeshRenderer componentsInChild in clone.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                if (componentsInChild.name == "Hair")
                {
                    UnityEngine.Object.Destroy(componentsInChild);
                }
            }
            //8
            GameObject hair = RRRLateLoadPrefabs.Clone("Hair8", "newHair", true, true);
            Transform hairTransform = hair.GetComponent<Transform>();
            hairTransform.localScale = new Vector3(5.7f, 6f, 5.7f);
            hairTransform.localPosition = new Vector3(0, 46.1f, 0.5f);
            //3
            GameObject beard = RRRLateLoadPrefabs.Clone("Beard5", "newBeard", true, true);
            Transform beardTransform = beard.GetComponent<Transform>();
            beardTransform.localScale = new Vector3(5.8f, 6f, 5.8f);
            beardTransform.localPosition = new Vector3(0, 45.65f, 0.32f);

            GameObject[] hairs = new GameObject[2]
            {
                hair,
                beard
            };
            Color hairColor = new Color(0.74f, 0.26f, 0.43f);
            foreach (Renderer rendererH in hairs.Where<GameObject>(i => i.IsNotNull()).SelectMany(i => i.GetComponentsInChildren<Renderer>(true)))
            {
                foreach (Material materialH in rendererH.materials)
                {
                    materialH.SetColor("_SkinColor", hairColor);
                }
            }

            foreach (Transform componentsInChildT in clone.GetComponentsInChildren<Transform>())
            {
                if (componentsInChildT.name == "Head")
                {
                    UnityEngine.Object.Instantiate(hair, componentsInChildT, true);
                } else if (componentsInChildT.name == "Jaw")
                {
                    UnityEngine.Object.Instantiate(beard, componentsInChildT, true);
                }
            }
        }
    }
}



