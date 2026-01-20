using SADungeon.Enemy;
using SADungeon.Player;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace SADungeon.FSM
{
    /// <summary>
    /// Shared data container (Blackboard) used by FSM states to access and store relevant information.
    /// The region "For dogs only" contains logic and data specific to dog AI behavior.
    /// </summary>
    public class Blackboard : MonoBehaviour
    {
        [HideInInspector] public NavMeshAgent navMeshAgent;
        
        public float rotateSpeed = 180f;         // Rotation speed in degrees per second
        public Transform stateOwnerTransform;    // The transform of the FSM owner
        public float targetReachedThreshold = 0.2f;   // Distance tolerance for reaching a destination

        [HideInInspector] public PlayerModel playerModel;
        
        [Header("Attack")] 
        public AttackData currentAttackData;
        [HideInInspector] public AttackBehaviour attackBehaviour;

        #region "For enemies only"
        [SerializeField]
        private bool isEnemy = true;

        // The target Transform that the enemy may chase or attack
        [HideInInspector] public Transform target;

        // Distance within which the enemy can attack
        public float attackRange = 1f;

        // Distance within which the enemy will start chasing the target
        public float chaseRange = 2.5f;

        // Movement speed when in normal patrol mode
        public float normalModeSpeed = 1f;
        
        // Minimum time the enemy idles in place
        public float idleTime;

        public ProjectileController projectilePrefab;

        public AttackData bossDefaultAttackData;
        public AttackData bossSlamAttackData;

        public void Initialize(NavMeshAgent agent)
        {
            playerModel = FindFirstObjectByType<PlayerModel>();
            
            attackBehaviour = GetComponent<AttackBehaviour>();
            if (attackBehaviour is null)
                Debug.LogWarning($"No attack behaviour attached to {gameObject.name}!");

            navMeshAgent = agent;
            navMeshAgent.speed = normalModeSpeed;
            
            if (playerModel == null)
                Debug.LogError("No player model found. Target remains empty.");

            target = playerModel?.transform;
        }
        #endregion
    }
}