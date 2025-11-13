using UnityEngine;

public class RangedEnemyAttackBehaviour : EnemyAttackBehaviour
{
    
    [SerializeField] 
    private ProjectileController projectilePrefab; // Projectile prefab to spawn
    
    public override void Attack(PlayerModel playerModel, AttackData attackData)
    {
        Transform target = playerModel.transform;
        
        ProjectileController projectile = Instantiate(projectilePrefab, playerModel.transform.position, Quaternion.identity);
        projectile.SetUp(target, attackData);
    }
}
