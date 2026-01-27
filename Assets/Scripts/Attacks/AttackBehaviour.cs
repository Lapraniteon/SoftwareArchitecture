using UnityEngine;

/// <summary>
/// Abstract class for attack behaviours to inherit.
/// </summary>

public abstract class AttackBehaviour : MonoBehaviour
{

    /// <summary>
    /// Attack a target with specified AttackData
    /// </summary>
    public abstract void Attack(Transform attacker, Transform target, AttackData attackData, string targetTag);

    /// <summary>
    /// Spawn VFX associated with AttackData at origin.
    /// </summary>
    public static void SpawnAttackVFX(Transform origin, AttackData attackData) // Spawn VFX if rotation doesn't matter
    {
        Instantiate(attackData.vfx, origin.position, Quaternion.identity);
    }

    /// <summary>
    /// Spawn VFX associated with AttackData at origin with a specified orientation towards a target.
    /// </summary>
    public static void SpawnAttackVFX(Transform origin, Transform lookAtTarget, AttackData attackData, bool useWorldUp = true) // Overload to spawn VFX if rotation matters
    {
        Vector3 direction = lookAtTarget.position - origin.position;
        
        if (useWorldUp)
            Instantiate(attackData.vfx, origin.position, Quaternion.LookRotation(direction, Vector3.up));
        else
            Instantiate(attackData.vfx, origin.position, Quaternion.LookRotation(direction));
    }
}
