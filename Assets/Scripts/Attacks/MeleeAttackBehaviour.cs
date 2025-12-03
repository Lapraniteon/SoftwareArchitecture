using SADungeon.Enemy;
using SADungeon.Player;
using UnityEngine;

public class MeleeAttackBehaviour : AttackBehaviour
{

    public override void Attack(Transform target, AttackData attackData, string targetTag)
    {
        target.GetComponent<PlayerModel>()?.GetHit(attackData);
        target.GetComponent<EnemyController>()?.GetHit(attackData);
    }
}
