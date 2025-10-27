using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerModel : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    [SerializeField] private int currentXP;
    [SerializeField] private int currentLevel;
    [SerializeField] private int currentHP;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void GoToDestination(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
    }
    
    public void ObtainXP(EventData eventData)
    {
        EnemyDieEventData enemyDieEventData = (EnemyDieEventData)eventData;

        currentXP += enemyDieEventData.enemy.XP;
    }
}
