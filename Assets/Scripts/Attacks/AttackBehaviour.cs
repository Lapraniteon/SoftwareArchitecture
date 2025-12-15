using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{

    public abstract void Attack(Transform attacker, Transform target, AttackData attackData, string targetTag);

    public static void SpawnAttackVFX(Transform origin, AttackData attackData) // Spawn VFX if rotation doesn't matter
    {
        Instantiate(attackData.vfx, origin.position, Quaternion.identity);
    }

    public static void SpawnAttackVFX(Transform origin, Transform lookAtTarget, AttackData attackData, bool useWorldUp = true) // Overload to spawn VFX if rotation matters
    {
        Vector3 direction = lookAtTarget.position - origin.position;
        
        if (useWorldUp)
            Instantiate(attackData.vfx, origin.position, Quaternion.LookRotation(direction, Vector3.up));
        else
            Instantiate(attackData.vfx, origin.position, Quaternion.LookRotation(direction));
    }
}
