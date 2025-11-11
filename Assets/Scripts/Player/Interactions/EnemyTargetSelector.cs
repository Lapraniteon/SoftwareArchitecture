using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetSelector : TargetSelector
{

    public EnemyController GetTarget() // Get targets for enemy types
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

private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            AddTarget(other.transform);
            Debug.Log($"Added {other.name} to targets");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetsInRange.Remove(other.transform);
            Debug.Log($"Removed {other.name} from targets");
        }
    }
}
