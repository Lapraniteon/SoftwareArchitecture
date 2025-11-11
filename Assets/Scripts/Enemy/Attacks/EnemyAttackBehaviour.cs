using UnityEngine;

public abstract class EnemyAttackBehaviour : MonoBehaviour
{
    public abstract void Attack(PlayerModel playerModel, AttackData attackData);
}
