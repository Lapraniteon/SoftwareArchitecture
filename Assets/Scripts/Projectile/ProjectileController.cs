using System;
using SADungeon.Enemy;
using SADungeon.Player;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Controls projectile movement and collisions with the target.
/// </summary>

public class ProjectileController : MonoBehaviour
{

    private AttackData _attackData;
    private Transform _target;
    private string _targetTag;
    [Tooltip("The travel (movement) speed of the projectile.")]
    [SerializeField]
    private float speed = 1f;
    [Tooltip("Time in seconds after which to despawn the projectile.")]
    [FormerlySerializedAs("duration")] [SerializeField]
    private float lifetime = 5f;
    
    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroy projectile after its lifetime has passed.
    }
    
    public void SetUp(Transform pTarget, AttackData pAttackData, string pTargetTag)
    {
        _target = pTarget;
        _targetTag = pTargetTag;
        _attackData = pAttackData;
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }
        
        // Move projectile forward
        Vector3 movement = (_target.position - transform.position).normalized * (speed * Time.deltaTime);
        transform.position += movement;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(_targetTag)) // Return if not collided with a target.
            return;
        
        if (_targetTag == Tags.Enemy)
        {
            Destroy(gameObject);
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            enemyController?.GetHit(_attackData);
            AttackBehaviour.SpawnAttackVFX(collision.transform, _attackData);
        }
        if (_targetTag == Tags.Player)
        {
            Destroy(gameObject);
            PlayerModel playerModel = collision.gameObject.GetComponent<PlayerModel>();
            playerModel?.GetHit(_attackData);
            AttackBehaviour.SpawnAttackVFX(collision.transform, _attackData);
        }
    }
}
