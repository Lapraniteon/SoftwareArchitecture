using SADungeon.Enemy;
using SADungeon.Player;
using UnityEngine;

public class MeleeAttackBehaviour : AttackBehaviour
{

    public override void Attack(Transform attacker, Transform target, AttackData attackData, string targetTag)
    {
        switch (targetTag)
        {
            case Tags.Player:
                target.GetComponent<PlayerModel>()?.GetHit(attackData);
                SpawnAttackVFX(attacker, target, attackData);
                break;
            case Tags.Enemy:
                target.GetComponent<EnemyController>()?.GetHit(attackData);
                break;
        }
    }
}
