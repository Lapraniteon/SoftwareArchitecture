using UnityEngine;
using System;
using SADungeon.FSM;
using UnityEngine.AI;

namespace SADungeon.Enemy
{

    /// <summary>
    /// Enemy controller that implements an FSM to control its behaviour and publishes events on creation and hit.
    /// Also supports dynamic switching of attack behaviours.
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        [Tooltip("The enemy's data to use.")]
        [SerializeField] private EnemyData enemyData;
        private Enemy enemy;

        [Tooltip("Should the enemy implement a boss FSM or a regular enemy's FSM?")]
        [SerializeField] private bool isBoss;
        private EnemyFSM enemyFSM;
        private BossFSM bossFSM;
        
        private NavMeshAgent navMeshAgent;
        
        [Tooltip("This enemy's operation data.")]
        [SerializeField] 
        private Blackboard blackboard;

        /// <summary>
        /// Fired when enemy instance is created before initialization.
        /// </summary>
        public event Action<Enemy> onEnemyCreated;
        
        /// <summary>
        /// Fired when an enemy's GetHit() method is called.
        /// </summary>
        public event Action<Enemy> onHit;

        void Start()
        {
            enemy = enemyData.CreateEnemy(); // Create enemy instance from enemy data.
            onEnemyCreated?.Invoke(enemy);
            
            navMeshAgent = GetComponent<NavMeshAgent>();

            if (navMeshAgent == null)
                Debug.LogWarning("No NavMeshAgent component attached! Disabling enemy.");
            
            blackboard.Initialize(navMeshAgent);

            if (isBoss) // Initialize the relevant blackboard based on if the enemy is a boss or not.
            {
                if (bossFSM == null)
                    bossFSM = new BossFSM(navMeshAgent, blackboard);
            }
            else
            {
                if (enemyFSM == null)
                    enemyFSM = new EnemyFSM(navMeshAgent, blackboard);
            }
            
            enemyFSM?.Enter();
            bossFSM?.Enter();
        }

        private void Update()
        {
            enemyFSM?.Step();
            bossFSM?.Step();
        }

        public void GetHit(AttackData attackData)
        {
            enemy.currentHP -= attackData.damage;

            if (enemy.currentHP < 0)
            {
                enemy.currentHP = 0;
            }

            Debug.Log("Enemy hit! Health: " + enemy.currentHP);

            onHit?.Invoke(enemy);
        }

        /// <summary>
        /// Destroys the current attack behaviour and switches to the passed in attack behaviour type.
        /// </summary>
        /// <param name="newBehaviour">The new attack behaviour.</param>
        public void SwitchAttackBehaviour(Type newBehaviour)
        {
            Destroy(blackboard.attackBehaviour);
            AttackBehaviour newComponent = (AttackBehaviour)gameObject.AddComponent(newBehaviour);

            if (newComponent is RangedAttackBehaviour rangedAttackBehaviour)
            {
                if (rangedAttackBehaviour != null)
                    rangedAttackBehaviour.projectilePrefab = blackboard.projectilePrefab;
            }
                
            blackboard.attackBehaviour = newComponent;
        }

        /// <summary>
        /// Switch to a different AttackData, useful for changing damage amounts at runtime.
        /// </summary>
        /// <param name="newData"></param>
        public void SwitchAttackData(AttackData newData)
        {
            blackboard.currentAttackData = newData;
        }
    }
}



