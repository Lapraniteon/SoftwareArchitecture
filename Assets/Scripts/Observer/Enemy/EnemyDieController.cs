using UnityEngine;

/// <summary>
/// Kill the enemy when it has 0 HP and handle related behaviours, publish
/// EnemyDieEvent
/// </summary>
[RequireComponent(typeof(LootSpawner))]
public class EnemyDieController : EnemyObserver
{
    [SerializeField]
    private GameEvent enemyDieEvent;
    private bool died = false;

    private LootSpawner lootSpawner;

    private void Start()
    {
        lootSpawner = GetComponent<LootSpawner>();
    }
    
    protected override void OnEnemyHit(Enemy enemy)
    {
        if (!died)
        {
            if (enemy.currentHP == 0)
            {
                enemyDieEvent.Publish(new EnemyDieEventData(enemy, enemyController.gameObject), enemyController.gameObject);
                died = true;
                lootSpawner.SpawnLoot(enemy.DropTable, enemyController.transform.position);
                Destroy(enemyController.gameObject);
            }
        }
    }
}
