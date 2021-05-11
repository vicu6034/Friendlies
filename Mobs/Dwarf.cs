//Groot
//v.05
using System;
using RRRCore;
using UnityEngine;
using System.Collections.Generic;

namespace GrootMod
{
    internal static class Dwarf
    {
        internal static string OriginalName = "Goblin";

        internal static void LateLoadDwarf(GameObject clone)
        {
            for (int index = 0; index < clone.transform.childCount; ++index)
                GameObject.Destroy(clone.transform.GetChild(index).gameObject);
            GameObject prefab = ZNetScene.instance.GetPrefab("Haldor");
            GameObject eyePos = (GameObject)null;
            for (int index = 0; index < prefab.transform.childCount; ++index)
            {
                GameObject gameObject = GameObject.Instantiate<GameObject>(prefab.transform.GetChild(index).gameObject, clone.transform);
                gameObject.name = gameObject.name.TrimCloneTag();
                if (gameObject.name == "EyePos")
                    eyePos = gameObject;
                if (gameObject.name == "Pipe")
                {
                    GameObject.Destroy(gameObject);
                }
            }

            VisEquipment component1 = clone.GetComponent<VisEquipment>();
            component1.m_bodyModel = clone.transform.Find("Haldor/HaldorTheTrader/Haldor").GetComponent<SkinnedMeshRenderer>();
            component1.m_leftHand = clone.transform.Find("Haldor/HaldorTheTrader/Armature/Root/Hip/Spine0/Spine1/Spine2/Shoulder.l/Upperarm.l/Underarm.l/Wrist.l/Shoulder.l.016/Shoulder.l.017/Shoulder.l.018").GetComponent<Transform>();
            component1.m_rightHand = clone.transform.Find("Haldor/HaldorTheTrader/Armature/Root/Hip/Spine0/Spine1/Spine2/Shoulder.r/Upperarm.r/Underarm.r/Wrist.r/Middle1.r").GetComponent<Transform>();
            component1.m_helmet = clone.transform.Find("Haldor/HaldorTheTrader/Armature/Root/Hip/Spine0/Spine1/Spine2/Head/Jaw").GetComponent<Transform>();
            component1.m_backShield = clone.transform.Find("Haldor/HaldorTheTrader/Armature/Root/Hip/Spine0/Spine1/Spine2/Shoulder.r/Upperarm.r/Underarm.r/Wrist.r/Pipe.001").GetComponent<Transform>();
            component1.m_backMelee = clone.transform.Find("Haldor/HaldorTheTrader/Armature/Root/Hip/Spine0/Spine1/Spine2/Shoulder.r/Upperarm.r/Underarm.r/Wrist.r/Pipe.001").GetComponent<Transform>();
            component1.m_backTwohandedMelee = clone.transform.Find("Haldor/HaldorTheTrader/Armature/Root/Hip/Spine0/Spine1/Spine2/Shoulder.r/Upperarm.r/Underarm.r/Wrist.r/Pipe.001").GetComponent<Transform>();
            component1.m_backBow = clone.transform.Find("Haldor/HaldorTheTrader/Armature/Root/Hip/Spine0/Spine1/Spine2/Shoulder.r/Upperarm.r/Underarm.r/Wrist.r/Pipe.001").GetComponent<Transform>();
            component1.m_backTool = clone.transform.Find("Haldor/HaldorTheTrader/Armature/Root/Hip/Spine0/Spine1/Spine2/Shoulder.r/Upperarm.r/Underarm.r/Wrist.r/Pipe.001").GetComponent<Transform>();
            component1.m_backAtgeir = clone.transform.Find("Haldor/HaldorTheTrader/Armature/Root/Hip/Spine0/Spine1/Spine2/Shoulder.r/Upperarm.r/Underarm.r/Wrist.r/Pipe.001").GetComponent<Transform>();
            /*
             component1.m_clothColliders = new CapsuleCollider[5]
             {
                 clone.transform.Find("Visual/Armature/Hips/ClothCollider").GetComponent<CapsuleCollider>(),
                 clone.transform.Find("Visual/Armature/Hips/LeftUpLeg/ClothCollider").GetComponent<CapsuleCollider>(),
                 clone.transform.Find("Visual/Armature/Hips/RightUpLeg/ClothCollider").GetComponent<CapsuleCollider>(),
                 clone.transform.Find("Visual/Armature/Hips/Spine/Spine1/ClothCollider (3)").GetComponent<CapsuleCollider>(),
                 clone.transform.Find("Visual/Armature/Hips/Spine/Spine1/Spine2/ClothCollider (4)").GetComponent<CapsuleCollider>()
             };
            */

            component1.m_models = prefab.GetComponent<VisEquipment>().m_models;
            component1.m_isPlayer = true;

            /*
            CapsuleCollider component2 = clone.GetComponent<CapsuleCollider>();
            component2.center = prefab.GetComponent<CapsuleCollider>().center;
            component2.radius = prefab.GetComponent<CapsuleCollider>().radius;
            component2.height = prefab.GetComponent<CapsuleCollider>().height;
            */
            //clone.GetComponent<Rigidbody>().interpolation = prefab.GetComponent<Rigidbody>().interpolation;
            ZSyncAnimation component3 = clone.GetComponent<ZSyncAnimation>();
            ZSyncAnimation component4 = prefab.GetComponent<ZSyncAnimation>();
            component3.m_syncBools = new List<string>((IEnumerable<string>)component4.m_syncBools);
            component3.m_syncFloats = new List<string>((IEnumerable<string>)component4.m_syncFloats);
            component3.m_syncInts = new List<string>((IEnumerable<string>)component4.m_syncInts);
            FootStep component5 = clone.GetComponent<FootStep>();
            FootStep component6 = prefab.GetComponent<FootStep>();
            component5.m_footstepCullDistance = component6.m_footstepCullDistance;
            component5.m_effects.Clear();
            foreach (FootStep.StepEffect effect in component6.m_effects)
                component5.m_effects.Add(effect);
            component5.m_feet[0] = clone.transform.Find("Haldor/HaldorTheTrader/Armature/Root/Hip/UpperLeg.l/LowerLeg.l/Foot.l/Foot2.l");
            component5.m_feet[1] = clone.transform.Find("Haldor/HaldorTheTrader/Armature/Root/Hip/UpperLeg.r/LowerLeg.r/Foot.r/Foot2.r");
            
            GameObject playerPrefab = RRRLateLoadPrefabs.Clone("Player", "newPlays", true, true);
            Humanoid component11 = clone.GetComponent<Humanoid>();
            Humanoid component21 = playerPrefab.GetComponent<Humanoid>();
            component11.m_eye = eyePos.transform;
            component11.m_crouchSpeed = component21.m_crouchSpeed;
            component11.m_walkSpeed = component21.m_walkSpeed;
            component11.m_speed = component21.m_speed;
            component11.m_turnSpeed = component21.m_turnSpeed;
            component11.m_runSpeed = component21.m_runSpeed;
            component11.m_runTurnSpeed = component21.m_runTurnSpeed;
            component11.m_acceleration = component21.m_acceleration;
            component11.m_jumpForce = component21.m_jumpForce;
            component11.m_jumpForceForward = component21.m_jumpForceForward;
            component11.m_swimDepth = component21.m_swimDepth;
            component11.m_swimSpeed = component21.m_swimSpeed;

            /*
            component11.m_damageModifiers = component21.m_damageModifiers.Clone();
            component11.m_defaultItems = new GameObject[0];
            component11.m_randomWeapon = new GameObject[0];
            component11.m_randomArmor = new GameObject[0];
            component11.m_randomShield = new GameObject[0];
            component11.m_randomSets = new Humanoid.ItemSet[0];
            */

            Character component = (Character)clone.GetComponent<Character>();
            BaseAI component2 = (BaseAI)clone.GetComponent<BaseAI>();

            component.m_name = "Dwarf";
            Character character1 = component;
            character1.m_health = 100;
            /*
            Character character2 = component;
            character2.m_acceleration = (float)(character2.m_acceleration * 0.8);
            Character character3 = component;
            character3.m_speed = (float)(character3.m_speed * 0.8);
            Character character4 = component;
            character4.m_walkSpeed = (float)(character4.m_walkSpeed * 0.8);
            Character character5 = component;
            character5.m_runSpeed = (float)(character5.m_runSpeed * 0.7);
            */
            Humanoid humanoid = (Humanoid)component;
            humanoid.m_defaultItems = (GameObject[])new GameObject[1]
            {
                RRRLateLoadPrefabs.Clone("SwordIron", "SwordIron2", true, true)
            //Dwarf.DesignAxeAttack(),
            //Dwarf.DesignAxeAttack2()
            };
            Character.Faction nFaction = Character.Faction.Players;
            humanoid.m_faction = nFaction;
            humanoid.m_randomSets = null;

            MonsterAI monsterAI = clone.GetComponent<MonsterAI>();
            monsterAI.m_viewRange = (int)25;
            monsterAI.m_spawnMessage = "Dwarf, ready!";
            monsterAI.m_deathMessage = "Oww!";
            monsterAI.m_enableHuntPlayer = false;

            Tameable tameable = (Tameable)clone.AddComponent<Tameable>();
            GameObject wolfObject = (GameObject)RRRLateLoadPrefabs.Clone("Wolf", "wolfClone9000", true, true);
            Tameable wolfTame = (Tameable)wolfObject.GetComponent<Tameable>();
            tameable.m_fedDuration = wolfTame.m_fedDuration;
            tameable.m_tamingTime = wolfTame.m_tamingTime;
            tameable.m_commandable = true;

            FootStep footStep = (FootStep)clone.AddComponent<FootStep>();
            GameObject greyObject = (GameObject)RRRLateLoadPrefabs.Clone("Player", "playClone", true, true);
            FootStep greyStep = (FootStep)greyObject.GetComponent<FootStep>();
            footStep = greyStep;

            CharacterDrop characterDrop = (CharacterDrop)clone.AddComponent<CharacterDrop>();
            characterDrop.m_drops.Clear();
            CharacterDrop.Drop drop = new CharacterDrop.Drop()
            {
                m_prefab = ZNetScene.instance.GetPrefab("Coins")
            };
            characterDrop.m_drops.Add(drop);

            //Dwarf.DesignAppearance(clone);
            
        }
        /*
        private static GameObject DesignAxeAttack()
        {
            return gameObject;
        }

        private static GameObject DesignAxeAttack2()
        {
            return gameObject;
        }

        private static void DesignAppearance(GameObject clone)
        {
            Transform transform = clone.transform;
            
        }

        */
    }
}


