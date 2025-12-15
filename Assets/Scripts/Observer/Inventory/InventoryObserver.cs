using SADungeon.Inventory;
using UnityEngine;

namespace SADungeon.Inventory
{

    public class InventoryObserver : MonoBehaviour
    {

        [SerializeField] protected Inventory inventory;

        protected void OnEnable()
        {
            if (inventory == null)
            {
                inventory = SingletonPlayerInventoryController.Instance.inventory;
            }
            
            inventory.onItemAdded += OnItemAdded;
            inventory.onItemRemoved += OnItemRemoved;
        }

        protected void OnDisable()
        {
            if (inventory == null)
                return;
            
            inventory.onItemAdded += OnItemAdded;
            inventory.onItemRemoved += OnItemRemoved;
        }
        
        protected virtual void OnItemAdded(Item item)
        {

        }

        protected virtual void OnItemRemoved(Item item)
        {

        }
    }
}