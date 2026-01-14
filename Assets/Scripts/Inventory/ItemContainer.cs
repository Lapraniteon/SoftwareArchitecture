using UnityEngine;
using System;

namespace SADungeon.Inventory
{
    /// <summary>
    /// An item container that invoke the onGetItem action to
    /// give an item, basicevent bus pattern implemention, which
    /// will be introduced in bootcamp 3.
    /// </summary>
    public class ItemContainer : MonoBehaviour
    {
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
