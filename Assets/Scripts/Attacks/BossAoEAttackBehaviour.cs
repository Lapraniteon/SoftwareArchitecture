using SADungeon.Enemy;
using SADungeon.Player;
using UnityEngine;

public class BossAoEAttackBehaviour : AttackBehaviour
{

    [SerializeField] private float areaOfEffect = 5f;
    
    public override void Attack(Transform attacker, Transform target, AttackData attackData, string targetTag)
    {
        if (targetTag != Tags.Player)
            return;

        if (Vector3.Distance(transform.position, target.position) <= areaOfEffect)
        {
            target.GetComponent<PlayerModel>()?.GetHit(attackData);
            SpawnAttackVFX(attacker, target, attackData);
        }
    }
}
