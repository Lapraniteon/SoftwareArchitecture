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
        
        public Vector3 currentTargetPosition;           // Target position for alignment or movement
        public float currentMoveSpeed;                  // Movement speed of the entity
        public float rotateSpeed = 180f;         // Rotation speed in degrees per second
        public Transform stateOwnerTransform;    // The transform of the FSM owner
        [HideInInspector] public float attackInterval = 0.5f;      // Time between attacks
        public float targetReachedThreshold = 0.2f;   // Distance tolerance for reaching a destination

        [HideInInspector] public PlayerModel playerModel;

        // TEMPORARY
        public float idleTime;
        
        [Header("Attack")] 
        public AttackData currentAttackData;
        [HideInInspector] public AttackBehaviour attackBehaviour;

        #region "For enemies only"
        [SerializeField]
        private bool isEnemy = true;

        // The target Transform that the enemy may chase or attack
        [HideInInspector] public Transform target;

        // List of waypoints for patrolling
        public Transform[] patrolWaypoints;

        // How long to wait at each waypoint
        public float[] waitingTimes;

        // Current waypoint index
        public int waypointIndex = 0;

        // Current wait time at a waypoint
        public float waitingTime;

        // Distance within which the enemy can attack
        public float attackRange = 1f;

        // Distance within which the enemy will start chasing the target
        public float chaseRange = 2.5f;

        // Movement speed when in normal patrol mode
        public float normalModeSpeed = 1f;

        // Movement speed when in alert mode (after detecting the player)
        public float alertModeSpeed = 2f;

        // Wait time at a waypoint when in alert mode
        public float alertModeWaitingTime = 1f;

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
            
            if (playerModel == null)
                Debug.LogError("No player model found.");

            target = playerModel?.transform;
        }
        
        // Subscribe to dog mode change events when the component is enabled.
        private void OnEnable()
        {
            EnemyModeController.onDogModeChanged += OnModeChanged;
        }

        // Unsubscribe from dog mode change events when the component is disabled.
        private void OnDisable()
        {
            EnemyModeController.onDogModeChanged -= OnModeChanged;
        }

        // Called when the dog's mode is changed (e.g., from NORMAL to ALERT).
        // Adjusts movement speed and waiting time accordingly.
        private void OnModeChanged(EnemyMode dogMode)
        {
            if (isEnemy)
            {
                // If the dog becomes alert, increase speed and set shorter wait time
                if (dogMode == EnemyMode.ALERT)
                {
                    currentMoveSpeed = alertModeSpeed;
                    waitingTime = alertModeWaitingTime;
                }
                // If the dog returns to normal, use default speed and waypoint-specific wait time
                else
                {
                    currentMoveSpeed = normalModeSpeed;
                    waitingTime = waitingTimes[waypointIndex];
                }

                navMeshAgent.speed = currentMoveSpeed;
                navMeshAgent.angularSpeed = rotateSpeed;
            }
        }

        /// <summary>
        /// Returns a random point near the current waypoint.
        /// Used when dogs are in Alert mode and seeking the player(move to a random target
        /// to try to detect the player).

        public Vector3 GetRandomPointAroundCurrentWaypoint()
        {
            // Generate a random offset in a 2D circle and scale it
            Vector2 randomOffset = UnityEngine.Random.insideUnitCircle.normalized * 2f;

            // Apply the offset to the current waypoint (on the XZ plane)
            return patrolWaypoints[waypointIndex].position +
                new Vector3(randomOffset.x, 0f, randomOffset.y);
        }
        #endregion
    }
}