using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using SADungeon.Inventory;
using UnityEngine;

namespace SADungeon.Quest
{
    
    /// <summary>
    /// Basic fetch quest logic. Keeps track of amount of items of a certain ItemData type that have been collected.
    /// </summary>
    
    public class FetchQuest : Quest
    {

        [Tooltip("The item type to collect.")]
        [SerializeField] private ItemData itemToCollectData;
        private Item itemToCollect;
        
        [Tooltip("The amount to collect for the quest to complete.")]
        [SerializeField] private int amountToCollect;
        
        [Tooltip("The current amount of items collected.")]
        [ReadOnly] [SerializeField] private int _currentAmount;

        protected override void Init()
        {
            if (SingletonPlayerInventoryController.Instance == null)
                return;
            
            SingletonPlayerInventoryController.Instance.inventory.onItemUpdated += OnInventoryUpdated;

            itemToCollect = itemToCollectData.CreateItem();
            
            OnInventoryUpdated(); // Check initial quest state
        }

        protected override void Kill() // Unbind events
        {
            if (SingletonPlayerInventoryController.Instance == null)
                return;
            
            SingletonPlayerInventoryController.Instance.inventory.onItemUpdated -= OnInventoryUpdated;
        }

        private void OnInventoryUpdated() // Called when the inventory contents have been updated.
        {
            if (completed) // Is quest already complete?
                return;
            
            _currentAmount = SingletonPlayerInventoryController.Instance.inventory.GetItems()
                .Count(item => item.Id.Equals(itemToCollect.Id)); // Check amount of items in inventory that match itemToCollectData.
            BroadcastProgressQuest(this);

            if (_currentAmount >= amountToCollect)
            {
                completed = true;
                BroadcastCompletedQuest(this);
            }
        }


        // Override string to display in UI.
        public override string ToDisplayString()
        {
            return $"Collect {amountToCollect} {itemToCollectData.itemName} ({_currentAmount}/{amountToCollect})";
        }
    }
}