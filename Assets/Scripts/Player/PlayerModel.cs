using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace SADungeon.Player
{

    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerModel : MonoBehaviour
    {
        [SerializeField] private PlayerData playerInitData;
        private Player player;

        public static event Action<Player> onPlayerInit;
        public static event Action<Player> onPlayerXPGained;
        public static event Action<Player> onPlayerHit;
        public static event Action<Player> onPlayerDead;
        public static event Action<Player> onPlayerLevelUp;

        private NavMeshAgent navMeshAgent;

        private void Start()
        {
            player = playerInitData.CreatePlayer();

            navMeshAgent = GetComponent<NavMeshAgent>();

            onPlayerInit?.Invoke(player);
        }

        public void GoToDestination(Vector3 destination)
        {
            navMeshAgent.SetDestination(destination);
        }

        public void OnEnemyKilled(EventData eventData)
        {
            // Only process XP if the hero hasn't reached the final level
            if (player.level <= player.NextLevelUpData.Length)
            {
                EnemyDieEventData enemyDieEventData = (EnemyDieEventData)eventData;

                player.currentXp += enemyDieEventData.enemy.XP;

                int previousLevel = player.level;

                // Check for multiple level-ups if XP is enough
                while (player.currentXp >= player.NextLevelUpData[player.level - 1].xpRequired)
                {
                    player.currentXp -= player.NextLevelUpData[player.level - 1].xpRequired;

                    player.MaxHP +=
                        player.NextLevelUpData[player.level - 1].baseHealthIncrease; // Increase player health
                    player.currentHP += player.NextLevelUpData[player.level - 1].baseHealthIncrease;

                    player.level++;

                    if (player.level > player.NextLevelUpData.Length)
                        break;
                }

                // Trigger xp gained event after xp value is set is correctly
                onPlayerXPGained?.Invoke(player);

                // Trigger level-up event if level increased
                if (player.level > previousLevel)
                    onPlayerLevelUp?.Invoke(player);
            }
        }

        public void GetHit(AttackData attackData)
        {
            player.currentHP -= attackData.damage;

            onPlayerHit?.Invoke(player);

            if (player.currentHP <= 0f)
            {
                onPlayerDead?.Invoke(player);
            }
        }
    }
}