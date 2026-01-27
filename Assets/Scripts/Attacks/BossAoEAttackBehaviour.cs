using SADungeon.Enemy;
using SADungeon.Player;
using UnityEngine;

/// <summary>
/// Attack behaviour that applies damage to a target in a specified area of effect.
/// </summary>

public class BossAoEAttackBehaviour : AttackBehaviour
{

    [SerializeField] private float areaOfEffect = 5f;
    
    public override void Attack(Transform attacker, Transform target, AttackData attackData, string targetTag)
    {
        if (targetTag != Tags.Player)
            return;

        if (Vector3.Distance(transform.position, target.position) <= areaOfEffect) // If target is within range
        {
            target.GetComponent<PlayerModel>()?.GetHit(attackData);
            SpawnAttackVFX(attacker, target, attackData);
        }
    }
}
