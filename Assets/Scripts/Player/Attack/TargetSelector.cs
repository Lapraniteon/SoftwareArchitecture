using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{

    [SerializeField]
    private List<EnemyController> targetsInRange = new ();
    
    public EnemyController GetTarget()
    {
        if (targetsInRange.Count > 0)
            return targetsInRange[0];

        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetsInRange.Add(other.GetComponent<EnemyController>());
            Debug.Log($"Added {other.name} to targets");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetsInRange.Remove(other.GetComponent<EnemyController>());
            Debug.Log($"Removed {other.name} from targets");
        }
    }
}
