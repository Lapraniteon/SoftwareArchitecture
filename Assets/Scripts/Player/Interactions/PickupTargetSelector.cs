using SADungeon.Inventory;
using UnityEngine;

public class PickupTargetSelector : TargetSelector
{

    public GroundPickup GetTarget() // Get targets for enemy types
    {
        if (targetsInRange.Count > 0)
            return targetsInRange[0].GetComponent<GroundPickup>();

        return null;
    }

    public void RemoveTarget(EventData eventData)
    {
        GroundPickupEventData groundPickupEventData = (GroundPickupEventData)eventData;
        targetsInRange.Remove(groundPickupEventData.pickupObject.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickupItem"))
        {
            targetsInRange.Add(other.transform);
            Debug.Log($"Added {other.name} to pickup targets");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickupItem"))
        {
            targetsInRange.Remove(other.transform);
            Debug.Log($"Removed {other.name} from pickup targets");
        }
    }
}
