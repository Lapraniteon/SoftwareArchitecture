using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerModel : MonoBehaviour
{
    [SerializeField] 
    private PlayerData playerData;
    private Player player;

    public event Action<Player> onPlayerInit;
    public event Action<Player> onPlayerXPGained;
    public event Action<Player> onPlayerLevelUp;
    
    private NavMeshAgent navMeshAgent;

    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        player = playerData.CreatePlayer();
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        onPlayerInit?.Invoke(player);
    }

    public void GoToDestination(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
    }
    
    public void OnEnemyKilled(EventData eventData)
    {
        EnemyDieEventData enemyDieEventData = (EnemyDieEventData)eventData;

        player.currentXp += enemyDieEventData.enemy.XP;
        
        onPlayerXPGained?.Invoke(player);
    }
}
