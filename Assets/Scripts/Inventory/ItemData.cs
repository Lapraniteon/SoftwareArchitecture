 using System;
using System.Collections.Generic;
using UnityEngine;

namespace SADungeon.Inventory
{
    /// <summary>
    /// This is the script for creating an ItemData scriptable object, which is
    /// "blueprint" to create item objects with the properties set up in the inspector,
    /// it is an implementation of factory pattern.defenseite
    /// </summary>
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
    public class ItemData : ScriptableObject
    {
        [Header("Unique id for each item")]
        public string id;

        [Header("Core properties")]
        public string itemName;
        public int attack;
        public int defense;
        public int healing;

        [Header("Visuals")]
        public Sprite itemIcon;
        
        [Header("Drop")]
        public GroundPickup groundPickupPrefab;

        public Item CreateItem()
        {
            return new Item(this);
        }
    }

    [Serializable]
    public class Item
    {
        [Header("Unique id for each item")]
        [SerializeField]
        private string id;
        public string Id => id;//This setup allows access to the private field 'id'
                               //while also allows it to be shown in the inspector

        [Header("Core properties")]
        [SerializeField]
        private string itemName;
        public string ItemName => itemName;
        [SerializeField]
        private int attack;
        public int Attack => attack;
        [SerializeField]
        private int defense;
        public int Defense => defense;
        [SerializeField] 
        private int healing;
        public int Healing => healing;

        [Header("Visuals")]
        public Sprite itemIcon;
        
        public GroundPickup groundPickupPrefab;

        public Item(ItemData itemData)
        {
            id = itemData.id;
            itemName = itemData.itemName;
            attack = itemData.attack;
            defense = itemData.defense;
            healing = itemData.healing;

            itemIcon = itemData.itemIcon;
            groundPickupPrefab = itemData.groundPickupPrefab;
        }
    }
}
