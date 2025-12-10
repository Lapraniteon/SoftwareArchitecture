using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace SADungeon.Inventory
{
    // This class presents items of an inventory in a ListView format.
    public class ListViewInventoryPresenter : InventoryPresenter
    {
        // Indicates whether this inventory belongs to the player
        [SerializeField]
        private bool belongsToPlayer = true;

        // Prefab used to display each item in the inventory list.
        [SerializeField]
        private ItemPresenter itemPresenterPrefab;

        // Parent transform under which item UI elements will be instantiated.
        public Transform listParent;

        private void OnEnable()
        {
            // If the inventory belongs to the player, get it from the player inventory controller singleton.
            if (belongsToPlayer)
                inventory = SingletonPlayerInventoryController.Instance.inventory;
            PresentInventory();
        }

        // Populates the inventory list UI with sorted items.
        public override void PresentInventory()
        {
            // Clear any existing item UI elements.
            ClearList();

            // Get sorted items from the inventory.
            Item[] items = inventory.GetItems();

            // Instantiate and present each item in the UI.
            for (int i = 0; i < items.Length; i++)
            {
                ItemPresenter itemPresenter = Instantiate<ItemPresenter>(itemPresenterPrefab);
                itemPresenter.PresentItem(items[i]);

                // Set the parent and scale for proper UI layout.
                itemPresenter.transform.SetParent(listParent);
                itemPresenter.transform.localScale = Vector3.one;
            }
        }

        // Clears all child item UI elements from the list except the parent itself.
        private void ClearList()
        {
            foreach (Transform transform in listParent.GetComponentsInChildren<Transform>())
            {
                if (transform != listParent)
                    Destroy(transform.gameObject);
            }
        }
    }
}
