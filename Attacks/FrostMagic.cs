using System;
using RRRCore;
using UnityEngine;

namespace Friendlies.Attacks
{
    public static class FrostMagic
    {
        public static GameObject Get(
            GameObject owner,
            string weaponName = "dragon_spit_shotgun",
            string panimationName = "sword_secondary"
            )
        {
            bool alreadyExisted = false;
            GameObject clone = RRRLateLoadPrefabs.CloneRepeatable(ref alreadyExisted, weaponName, "FrostMagic", regOdb: true);
            if (alreadyExisted)
                return clone;

            ItemDrop component = clone.GetComponent<ItemDrop>();
            if ((UnityEngine.Object)component == (UnityEngine.Object)null)
                throw new NullReferenceException("No ItemDrop component in prefab: " + weaponName);
            ItemDrop.ItemData.SharedData shared = component.m_itemData.m_shared;

            shared.m_name = "Frost Magic";
            shared.m_description = "A Magical Frost Attack";
            shared.m_useDurability = false;
            shared.m_ammoType = "";

            shared.m_damages.m_frost = 15;
            shared.m_attack.m_attackOriginJoint = "RightHandMiddle2";
            GameObject lilFrost = shared.m_attack.m_attackProjectile;
            Transform transform = lilFrost.transform;
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            shared.m_attack.m_attackProjectile = lilFrost;

            return clone;
        }
    }
}
