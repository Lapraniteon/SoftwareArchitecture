using SADungeon.Enemy;
using SADungeon.Player;
using UnityEngine;

public class MeleeAttackBehaviour : AttackBehaviour
{

    [SerializeField] private float attackRange;
    
    public override void Attack(Transform target, AttackData attackData)
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
        {
            target.GetComponent<PlayerModel>()?.GetHit(attackData);
            target.GetComponent<EnemyController>()?.GetHit(attackData);
        }
    }
}
