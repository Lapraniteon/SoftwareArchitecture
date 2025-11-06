using SADungeon.Inventory;
using UnityEngine;

public class GroundPickup : ItemContainer
{
    [SerializeField] 
    private GameEvent groundPickupEvent;
    
    public void PickupItem()
    {
        Item item = GiveItem();
        
        groundPickupEvent.Publish(new GroundPickupEventData(item, this.gameObject), this.gameObject);
        Destroy(this.gameObject);
    }
}
