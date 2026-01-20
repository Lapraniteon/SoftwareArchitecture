using System;
using SADungeon.Enemy;
using SADungeon.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

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
        public static event Action<Player> onPlayerHeal;
        public static event Action<Player> onPlayerHealthChanged;

        private NavMeshAgent navMeshAgent;

        [FormerlySerializedAs("currentHealingItem")]
        [Header("Healing")]
        [SerializeField] private ItemData currentHealingItemData;
        [SerializeField] private ParticleSystem healingVFX;

        private void OnEnable()
        {
            GlobalInputManager.onHealButtonPressed += OnHealButtonPressed;
        }
        
        private void OnDisable()
        {
            GlobalInputManager.onHealButtonPressed -= OnHealButtonPressed;
        }

        private void OnHealButtonPressed()
        {
            if (currentHealingItemData == null)
            {
                Debug.LogError("No healing item selected.");
                return;
            }

            if (player.currentHP >= player.maxHP) // Don't heal if already max health
                return;
            
            if (SingletonPlayerInventoryController.Instance.inventory.RemoveItem(currentHealingItemData))
            {
                // Healing was successful
                player.currentHP += currentHealingItemData.healing;
                if (player.currentHP > player.maxHP) player.currentHP = player.maxHP;
                Instantiate(healingVFX, transform.position, Quaternion.identity);
                onPlayerHeal?.Invoke(player);
                onPlayerHealthChanged?.Invoke(player);
            }
        }

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

                ProcessXP(enemyDieEventData.enemy.XP);
            }
        }

        public void ProcessXP(int amount)
        {
            if (player.level <= player.NextLevelUpData.Length)
            {
                player.currentXP += amount;

                int previousLevel = player.level;

                // Check for multiple level-ups if XP is enough
                while (player.currentXP >= player.NextLevelUpData[player.level - 1].xpRequired)
                {
                    player.currentXP -= player.NextLevelUpData[player.level - 1].xpRequired;

                    player.maxHP +=
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
            onPlayerHealthChanged?.Invoke(player);

            if (player.currentHP <= 0f)
            {
                onPlayerDead?.Invoke(player);
            }
        }
    }
}