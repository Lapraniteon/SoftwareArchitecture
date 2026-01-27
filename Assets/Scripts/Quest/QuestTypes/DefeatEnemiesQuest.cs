using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using SADungeon.Enemy;
using UnityEngine;

namespace SADungeon.Quest
{
    
    /// <summary>
    /// Basic fetch quest logic. Keeps track of amount of enemies of a certain EnemyData type that have been defeated.
    /// </summary>
    
    public class DefeatEnemiesQuest : Quest
    {

        [Tooltip("The enemy type to defeat.")]
        [SerializeField] private EnemyData enemyToDefeatData;
        private Enemy.Enemy enemyToDefeat;
        
        [Tooltip("The amount to defeat for the quest to complete.")]
        [SerializeField] private int amountToDefeat;
        
        [Tooltip("The current amount of enemies defeated.")]
        [ReadOnly] [SerializeField] private int _currentAmount;

        protected override void Init()
        {
            enemyToDefeat = enemyToDefeatData.CreateEnemy();
        }

        protected override void Kill()
        {

        }

        public void OnEnemyDefeated(EventData eventData)
        {

            if (completed)
                return;

            EnemyDieEventData enemyDieEventData = (EnemyDieEventData)eventData;

            if (enemyDieEventData.enemy.Id == enemyToDefeatData.id) // If defeated enemy matches enemy to defeat.
            {
                ProgressQuest(1);
            }

            if (_currentAmount >= amountToDefeat)
            {
                completed = true;
                BroadcastCompletedQuest(this);
            }
        }
        
        // Override function to increase the current amount of enemies defeated.
        public override void ProgressQuest(int amount)
        {
            _currentAmount += amount;
            BroadcastProgressQuest(this);
        }

        // Override string to display in UI.
        public override string ToDisplayString()
        {
            return $"Defeat {amountToDefeat} {enemyToDefeatData.enemyName} ({_currentAmount}/{amountToDefeat})";
        }
    }
}