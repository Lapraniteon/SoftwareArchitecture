using UnityEngine;

public class RangedAttackBehaviour : AttackBehaviour
{
    
    [SerializeField] 
    public ProjectileController projectilePrefab; // Projectile prefab to spawn
    
    public override void Attack(Transform target, AttackData attackData, string targetTag)
    {
        ProjectileController projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.SetUp(target, attackData, targetTag);
    }
}
