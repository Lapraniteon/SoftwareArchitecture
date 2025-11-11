using UnityEngine;

public class MeleeEnemyAttackBehaviour : EnemyAttackBehaviour
{

    [SerializeField] private float attackRange;
    
    public override void Attack(PlayerModel playerModel, AttackData attackData)
    {
        if (Vector3.Distance(transform.position, playerModel.transform.position) <= attackRange)
        {
            playerModel.GetHit(attackData.damage);
        }
    }
}
