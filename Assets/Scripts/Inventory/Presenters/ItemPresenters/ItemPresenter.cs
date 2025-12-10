using UnityEngine;

namespace SADungeon.Inventory {

    /// <summary>
    /// Abstract item presenter, an item presenter presents an item as different
    /// visuals in the game.
    /// </summary>
    public abstract class ItemPresenter : MonoBehaviour
    {
        public abstract void PresentItem(Item item);
    }
}
