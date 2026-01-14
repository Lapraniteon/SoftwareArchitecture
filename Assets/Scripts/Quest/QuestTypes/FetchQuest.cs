using System;
using System.Collections.Generic;
using System.Linq;
using SADungeon.Inventory;
using UnityEngine;

namespace SADungeon.Quest
{

    public class FetchQuest : Quest
    {

        [SerializeField] private ItemData itemToCollectData;
        private Item itemToCollect;
        [SerializeField] private int amountToCollect;
        [SerializeField] private int _currentAmount;

        protected override void Init()
        {
            if (SingletonPlayerInventoryController.Instance == null)
                return;
            
            SingletonPlayerInventoryController.Instance.inventory.onItemUpdated += OnInventoryUpdated;

            itemToCollect = itemToCollectData.CreateItem();
            
            OnInventoryUpdated(); // Check initial quest state
        }

        protected override void Kill()
        {
            if (SingletonPlayerInventoryController.Instance == null)
                return;
            
            SingletonPlayerInventoryController.Instance.inventory.onItemUpdated -= OnInventoryUpdated;
        }

        private void OnInventoryUpdated()
        {
            if (completed)
                return;
            
            _currentAmount = SingletonPlayerInventoryController.Instance.inventory.GetItems()
                .Count(item => item.Id.Equals(itemToCollect.Id));
            BroadcastProgressQuest(this);

            if (_currentAmount >= amountToCollect)
            {
                completed = true;
                BroadcastCompletedQuest(this);
            }
        }


        public override string ToDisplayString()
        {
            return $"Collect {amountToCollect} {itemToCollectData.itemName} ({_currentAmount}/{amountToCollect})";
        }
    }
}