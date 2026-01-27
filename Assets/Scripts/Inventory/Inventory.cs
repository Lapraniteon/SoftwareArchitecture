using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SADungeon.Inventory
{
    
    /// <summary>
    /// An Inventory instance that stores the player's inventory in a list, with methods to add and remove items.
    /// </summary>
    
    public class Inventory : MonoBehaviour
    {
        // List of item data assets used to generate actual items at runtime.
        [SerializeField]
        private List<ItemData> itemDatas;

        // List of instantiated items currently in the inventory.
        [SerializeReference]
        private List<Item> items;

        /// <summary>
        /// Fired when an item is added to the inventory.
        /// </summary>
        public event Action<Item> onItemAdded;
        /// <summary>
        /// Fired when an item is removed from the inventory.
        /// </summary>
        public event Action<Item> onItemRemoved;
        /// <summary>
        /// Fired when the inventory's contents are updated.
        /// </summary>
        public event Action onItemUpdated;

        private void Awake()
        {
            GenerateInventory(); // Create items based on itemDatas.
        }

        // Instantiates items based on the item data list.
        private void GenerateInventory()
        {
            items = new List<Item>();
            foreach (ItemData itemData in itemDatas)
            {
                AddItem(itemData);
            }
        }

        // Adds an item to the inventory.
        public void AddItem(ItemData itemData)
        {
            Item item = itemData.CreateItem();
            items.Add(item);
            onItemAdded?.Invoke(item);
            onItemUpdated?.Invoke();
        }
        
        // Helper method for unit testing
        public void RemoveItemVoid(ItemData itemData) => RemoveItem(itemData);

        // Removes an item from the inventory.
        public bool RemoveItem(ItemData itemData)
        {
            Item item = itemData.CreateItem();
            
            if (items.Any(pItem => pItem.Id.Equals(item.Id)))
            {
                Item existingItem = items.FirstOrDefault(i => i.Id == item.Id);
                if (existingItem == null)
                    return false;
                
                items.Remove(existingItem);
                onItemRemoved?.Invoke(item);
                onItemUpdated?.Invoke();
                return true;
            }

            return false;
        }
        
        public Item[] GetItems()
        {
            return items.ToArray();
        }
    }
}