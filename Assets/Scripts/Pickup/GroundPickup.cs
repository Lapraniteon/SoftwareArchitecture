using SADungeon.Inventory;
using UnityEngine;

/// <summary>
/// Class responsible for firing its pickup on the event bus.
/// </summary>

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
