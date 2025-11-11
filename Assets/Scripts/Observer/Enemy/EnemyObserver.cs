using UnityEngine;

/// <summary>
/// The abstract class for concrete enemy observers, the abstract class subscribes to onEnemyCreated
/// for initialization and onEnemyHit for getting enemy hit notification and acting accordingly
/// </summary>

public abstract class EnemyObserver : MonoBehaviour
{
    [SerializeField]
    protected EnemyController enemyController;

    protected virtual void OnEnable()
    {
        enemyController.onEnemyCreated += OnEnemyCreated;
        enemyController.onHit += OnEnemyHit;
    }

    protected virtual void OnDisable()
    {
        enemyController.onEnemyCreated -= OnEnemyCreated;
        enemyController.onHit -= OnEnemyHit;
    }

    protected virtual void OnEnemyCreated(Enemy enemy)
    {
        
    }

    protected virtual void OnEnemyHit(Enemy enemy)
    {
        
    }
}
