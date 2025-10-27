using UnityEngine;

/// <summary>
/// Kill the enemy when it has 0 HP and handle related behaviours, publish
/// EnemyDieEvent
/// </summary>
public class EnemyDieController : EnemyObserver
{
    [SerializeField]
    private GameEvent enemyDieEvent;
    private bool died = false;


    protected override void OnEnemyCreated(Enemy enemy)
    {
        // No implementation needed for now
    }

    protected override void OnEnemyHit(Enemy enemy)
    {
        if (!died)
        {
            if (enemy.currentHP == 0)
            {
                enemyDieEvent.Publish(new EnemyDieEventData(enemy, enemyController.gameObject), enemyController.gameObject);
                died = true;
                Destroy(enemyController.gameObject);
            }
        }
    }
}
