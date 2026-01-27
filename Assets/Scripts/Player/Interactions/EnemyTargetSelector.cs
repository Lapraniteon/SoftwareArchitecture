using System.Collections.Generic;
using SADungeon.Enemy;
using UnityEngine;

namespace SADungeon.Player
{
    
    /// <summary>
    /// Class responsible for detecting and storing the enemies in range and passing a target to the attackBehaviour.
    /// </summary>
    
    public class EnemyTargetSelector : TargetSelector
    {

        public EnemyController GetTarget() // Return one of the targets currently in range.
        {
            if (targetsInRange.Count > 0)
                return targetsInRange[0].GetComponent<EnemyController>();

            return null;
        }

        public void RemoveTarget(EventData eventData)
        {
            EnemyDieEventData enemyDieEventData = (EnemyDieEventData)eventData;
            targetsInRange.Remove(enemyDieEventData.enemyObject.transform);
        }

        private void AddTarget(Transform target)
        {
            if (!targetsInRange.Contains(target))
                targetsInRange.Add(target);
        }

        private void OnTriggerEnter(Collider other) // Add a target to the list when it enters range.
        {
            if (other.CompareTag("Enemy"))
            {
                AddTarget(other.transform);
                Debug.Log($"Added {other.name} to targets");
            }
        }

        private void OnTriggerExit(Collider other) // Remove a target from the list when it leaves range.
        {
            if (other.CompareTag("Enemy"))
            {
                targetsInRange.Remove(other.transform);
                Debug.Log($"Removed {other.name} from targets");
            }
        }
    }
}