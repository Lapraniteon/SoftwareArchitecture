using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.Serialization;

namespace SADungeon.Inventory
{

    public class InventoryHPItemDisplay : InventoryObserver
    {
        
        [SerializeField] private TextMeshProUGUI amountText;

        [SerializeField] private ItemData itemToCheck;
        
        protected override void OnItemAdded(Item item)
        {
            UpdateDisplay(item);
        }
        
        protected override void OnItemRemoved(Item item)
        {
            UpdateDisplay(item);
        }
        
        private void UpdateDisplay(Item item)
        {
            Debug.Log("Updating display");
            
            if (!item.Id.Equals(itemToCheck.id))
                return;
            
            Debug.Log("Hi");
            
            //int amount = inventory.GetItems().Count(_ => item.Id.Equals(itemToCheck.id));
            int amount = inventory.Items.Length;
            
            Debug.Log(amount);
            
            amountText.text = amount.ToString();
        }
    }
}