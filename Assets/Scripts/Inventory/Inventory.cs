using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SADungeon.Inventory
{
    /// <summary>
    /// A inventory class that uses Strategy pattern to quickly switch different sorting
    /// methods for its items.
    /// </summary>
    public class Inventory : MonoBehaviour
    {
        // List of item data assets used to generate actual items at runtime.
        [SerializeField]
        private List<ItemData> itemDatas;

        // List of instantiated items currently in the inventory.
        [SerializeReference]
        private List<Item> items;

        // Public read-only property to access a copy of the items list.
        public Item[] Items => items.ToArray();

        public event Action<Item> onItemAdded;
        public event Action<Item> onItemRemoved;
        public event Action onItemUpdated;

        // MonoBehaviour-based sorting strategies.
        // [SerializeField]
        // private ItemSortingStrategy[] itemSortingStrategies;

        // Index of the currently active sorting strategy.
        // [SerializeField]
        // private int strategyIndex = 0;

        private void Awake()
        {
            GenerateInventory();             // Create items based on itemDatas.
            //LoadItemSortingStrategies();     // Find sorting strategies attached as components.
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
        
        /*
        #region "Strategy Pattern Implementation"
        // Loads sorting strategy components from child objects.
        private void LoadItemSortingStrategies()
        {
            itemSortingStrategies = GetComponentsInChildren<ItemSortingStrategy>();
        }

        // Returns the items sorted according to the current strategy.
        public Item[] GetSortedItems()
        {
            // If no sorting strategies exist, return the unsorted list.
            if (itemSortingStrategies.Length == 0)
            {
                return items.ToArray();
            }
            else
            {
                return itemSortingStrategies[strategyIndex].GetSortedItems(items);
            }
        }*/
        
        public Item[] GetItems()
        {
            return items.ToArray();
        }

        /*
        // Sets the current sorting strategy by index.
        public void SetSortingStrategy(int pIndex)
        {
            strategyIndex = pIndex;
        }

        // Cycles to the next sorting strategy (loops back to 0 if at the end).
        public void NextSortingStrategy()
        {
            if (strategyIndex == itemSortingStrategies.Length - 1)
            {
                strategyIndex = 0;
            }
            else
            {
                strategyIndex++;
            }
        }

        // Cycles to the previous sorting strategy (loops to last if at the start).
        public void PreviousSortingStrategy()
        {
            if (strategyIndex == 0)
            {
                strategyIndex = itemSortingStrategies.Length - 1;
            }
            else
            {
                strategyIndex--;
            }
        }

        // Returns the name of the currently selected sorting strategy.
        public string GetCurrentStrategyName()
        {
            return itemSortingStrategies[strategyIndex].StrategyName;
        }
        #endregion
        */
    }
}