using SADungeon.Inventory;
using UnityEngine;

namespace SADungeon.Player
{
    
    /// <summary>
    /// Class responsible for detecting and storing the pickups in range.
    /// </summary>
    
    public class PickupTargetSelector : TargetSelector
    {

        public GroundPickup GetTarget() // // Return one of the GroundPickups currently in range.
        {
            if (targetsInRange.Count > 0)
                return targetsInRange[0].GetComponent<GroundPickup>();

            return null;
        }

        public void RemoveTarget(EventData eventData) // Remove target when item is picked up.
        {
            GroundPickupEventData groundPickupEventData = (GroundPickupEventData)eventData;
            targetsInRange.Remove(groundPickupEventData.pickupObject.transform);
        }

        private void OnTriggerEnter(Collider other) // Add target when pickup enters range.
        {
            if (other.CompareTag("PickupItem"))
            {
                targetsInRange.Add(other.transform);
                Debug.Log($"Added {other.name} to pickup targets");
            }
        }

        private void OnTriggerExit(Collider other) // Remove target when pickup leaves range.
        {
            if (other.CompareTag("PickupItem"))
            {
                targetsInRange.Remove(other.transform);
                Debug.Log($"Removed {other.name} from pickup targets");
            }
        }
    }
}