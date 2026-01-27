using UnityEngine;
using System;

namespace SADungeon.Inventory
{
    /// <summary>
    /// An item container that invoke the onGetItem action to
    /// give an item.
    /// </summary>
    public class ItemContainer : MonoBehaviour
    {
        /// <summary>
        /// Fires when item gets picked up.
        /// </summary>
        public static Action<ItemData> onGetItem;
        [SerializeField]
        public ItemData itemData;

        public Item GiveItem()
        {
            Item item = itemData.CreateItem();
            onGetItem?.Invoke(itemData);
            return item;
        }
    }
}
