using System;
using SADungeon.Enemy;
using SADungeon.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace SADungeon.Player
{
    
    /// <summary>
    /// Class responsible for handling internal player logic, such as XP and HP handling.
    /// Also broadcasts many events based on player actions.
    /// </summary>
    
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerModel : MonoBehaviour
    {
        [Tooltip("The PlayerData to initialize the player with.")]
        [SerializeField] private PlayerData playerInitData;
        private Player player;

        /// <summary>
        /// Fired when the player has finished initializing.
        /// </summary>
        public static event Action<Player> onPlayerInit;
        /// <summary>
        /// Fired after the player has gained XP.
        /// </summary>
        public static event Action<Player> onPlayerXPGained;
        /// <summary>
        /// Fired after the player has gotten hit.
        /// </summary>
        public static event Action<Player> onPlayerHit;
        /// <summary>
        /// Fired when the player's health is 0 or below as a result of getting hit.
        /// </summary>
        public static event Action<Player> onPlayerDead;
        /// <summary>
        /// Fired when the player's level has increased after getting XP.
        /// </summary>
        public static event Action<Player> onPlayerLevelUp;
        /// <summary>
        /// Fired when the player has healed.
        /// </summary>
        public static event Action<Player> onPlayerHeal;
        /// <summary>
        /// Fired when the player's health has changed.
        /// </summary>
        public static event Action<Player> onPlayerHealthChanged;

        private NavMeshAgent navMeshAgent;

        [Header("Healing")]
        [Tooltip("The healing item the player will use from the inventory to heal.")]
        [SerializeField] private ItemData currentHealingItemData;
        [Tooltip("The VFX to spawn at the player position when the heal button is pressed.")]
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
            
            if (SingletonPlayerInventoryController.Instance.inventory.RemoveItem(currentHealingItemData)) // Remove one healing item from the inventory
            {
                // Healing was successful
                player.currentHP += currentHealingItemData.healing;
                if (player.currentHP > player.maxHP) player.currentHP = player.maxHP; // Clamp player HP to max.
                
                Instantiate(healingVFX, transform.position, Quaternion.identity); // Spawn healing VFX
                
                onPlayerHeal?.Invoke(player);
                onPlayerHealthChanged?.Invoke(player);
            }
        }

        private void Start()
        {
            player = playerInitData.CreatePlayer(); // Create player instance from intializaton data.

            navMeshAgent = GetComponent<NavMeshAgent>();

            onPlayerInit?.Invoke(player);
        }

        public void GoToDestination(Vector3 destination)
        {
            navMeshAgent.SetDestination(destination);
        }

        public void OnEnemyKilled(EventData eventData)
        {
            // Only process XP if the player hasn't reached the final level yet.
            if (player.level <= player.NextLevelUpData.Length)
            {
                EnemyDieEventData enemyDieEventData = (EnemyDieEventData)eventData;

                ProcessXP(enemyDieEventData.enemy.XP);
            }
        }

        public void ProcessXP(int amount)
        {
            if (player.level <= player.NextLevelUpData.Length) // If the player has not reached max. level.
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

        public void GetHit(AttackData attackData) // Called by the attack behaviour that attacked the player.
        {
            player.currentHP -= attackData.damage;

            onPlayerHit?.Invoke(player);
            onPlayerHealthChanged?.Invoke(player);

            if (player.currentHP <= 0f) // Player is dead.
            {
                onPlayerDead?.Invoke(player);
            }
        }
    }
}