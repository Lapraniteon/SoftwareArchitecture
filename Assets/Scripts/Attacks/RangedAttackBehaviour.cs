using UnityEngine;

public class RangedAttackBehaviour : AttackBehaviour
{
    
    [SerializeField] 
    private ProjectileController projectilePrefab; // Projectile prefab to spawn
    
    public override void Attack(Transform target, AttackData attackData)
    {
        ProjectileController projectile = Instantiate(projectilePrefab, target.transform.position, Quaternion.identity);
        projectile.SetUp(target, attackData);
    }
}
