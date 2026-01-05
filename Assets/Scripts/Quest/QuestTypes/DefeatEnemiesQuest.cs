using System.Collections.Generic;
using System.Linq;
using SADungeon.Enemy;
using UnityEngine;

namespace SADungeon.Quest
{

    public class DefeatEnemiesQuest : Quest
    {

        [SerializeField] private EnemyData enemyToDefeatData;
        private Enemy.Enemy enemyToDefeat;
        [SerializeField] private int amountToDefeat;
        [SerializeField] private int _currentAmount;

        protected override void Init()
        {
            enemyToDefeat = enemyToDefeatData.CreateEnemy();
            
            //OnInventoryUpdated(); // Check initial quest state
        }

        protected override void Kill()
        {
            
        }

        public void OnEnemyDefeated(EventData eventData)
        {

            if (completed)
                return;
            
            EnemyDieEventData enemyDieEventData = (EnemyDieEventData)eventData;

            if (enemyDieEventData.enemy.Id == enemyToDefeatData.id)
            {
                _currentAmount++;
                BroadcastProgressQuest(this);
            }

            if (_currentAmount >= amountToDefeat)
            {
                completed = true;
                BroadcastCompletedQuest(this);
            }
        }


        public override string ToDisplayString()
        {
            return $"Defeat {amountToDefeat} {enemyToDefeatData.enemyName} ({_currentAmount}/{amountToDefeat})";
        }
    }
}