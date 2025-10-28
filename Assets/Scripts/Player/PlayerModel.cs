using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerModel : MonoBehaviour
{
    [SerializeField] 
    private PlayerData playerInitData;
    private Player player;

    public event Action<Player> onPlayerInit;
    public event Action<Player> onPlayerXPGained;
    public event Action<Player> onPlayerLevelUp;
    
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
        if (player.level <= player.XpRequirementsForNextLevel.Length)
        {
            EnemyDieEventData enemyDieEventData = (EnemyDieEventData)eventData;

            player.currentXp += enemyDieEventData.enemy.XP;

            int previousLevel = player.level;
            
            // Check for multiple level-ups if XP is enough
            while (player.currentXp >= player.XpRequirementsForNextLevel[player.level - 1])
            {
                player.currentXp -= player.XpRequirementsForNextLevel[player.level - 1];

                player.level++;

                if (player.level > player.XpRequirementsForNextLevel.Length)
                    break;
            }
        
            // Trigger xp gained event after xp value is set is correctly
            onPlayerXPGained?.Invoke(player);
            
            // Trigger level-up event if level increased
            if (player.level > previousLevel)
                onPlayerLevelUp?.Invoke(player);
        }

        
    }
}
