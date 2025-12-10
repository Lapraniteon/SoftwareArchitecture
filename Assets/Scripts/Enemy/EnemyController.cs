using UnityEngine;
using System;
using SADungeon.FSM;
using UnityEngine.AI;

namespace SADungeon.Enemy
{

    /// <summary>
    /// Simple enemy controller that publish onEnemyCreated and onHit events when
    /// it's created and hit.
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyData enemyData;
        private Enemy enemy;

        [SerializeField] private bool isBoss;
        private EnemyFSM enemyFSM;
        private BossFSM bossFSM;
        
        [SerializeField] 
        private NavMeshAgent navMeshAgent;
        [SerializeField] 
        private Blackboard blackboard;

        public event Action<Enemy> onEnemyCreated;
        public event Action<Enemy> onHit;

        void Start()
        {
            enemy = enemyData.CreateEnemy();
            onEnemyCreated?.Invoke(enemy);
            
            blackboard.Initialize(navMeshAgent);

            if (isBoss)
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

        public void SwitchAttackData(AttackData newData)
        {
            blackboard.currentAttackData = newData;
        }
    }
}



