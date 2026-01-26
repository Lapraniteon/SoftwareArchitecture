using UnityEngine;

namespace SADungeon.Inventory {
    /// <summary>
    /// This class presents inventory items to GUI, it can change item's sorting
    /// order by using different sorting strategies of the inventory class.
    /// </summary>
    public abstract class InventoryPresenter : MonoBehaviour
    {
        [SerializeField]
        protected Inventory inventory;

        public abstract void PresentInventory();
    }
}
