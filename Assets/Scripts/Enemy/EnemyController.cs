using UnityEngine;
using System;

/// <summary>
/// Simple enemy controller that publish onEnemyCreated and onHit events when
/// it's created and hit.
/// </summary>
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyData enemyData;
    private Enemy enemy;

    public event Action<Enemy> onEnemyCreated;
    public event Action<Enemy> onHit;

    void Start()
    {
        enemy = enemyData.CreateEnemy();
        onEnemyCreated?.Invoke(enemy);
    }

    public void GetHit()
    {
        enemy.currentHP -= 1;
        if(enemy.currentHP < 0)
        {
            enemy.currentHP = 0;
        }
        
        Debug.Log("Enemy hit! Health: " + enemy.currentHP);

        onHit?.Invoke(enemy);
    }
}




