using SADungeon.Enemy;
using SADungeon.Player;
using UnityEngine;

/// <summary>
/// Attack behaviour that directly applies damage to a target.
/// </summary>

public class MeleeAttackBehaviour : AttackBehaviour
{

    public override void Attack(Transform attacker, Transform target, AttackData attackData, string targetTag)
    {
        switch (targetTag) // Call GetHit on different classes depending on the target.
        {
            case Tags.Player:
                target.GetComponent<PlayerModel>()?.GetHit(attackData);
                SpawnAttackVFX(attacker, target, attackData);
                break;
            case Tags.Enemy:
                target.GetComponent<EnemyController>()?.GetHit(attackData);
                SpawnAttackVFX(target, target, attackData);
                break;
        }
    }
}
