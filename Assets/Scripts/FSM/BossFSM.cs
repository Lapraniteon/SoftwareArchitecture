using SADungeon.Enemy;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

namespace SADungeon.FSM
{

    /// <summary>
    /// A finite state machine that controls the enemy behaviour
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class BossFSM : FSM
    {
        private NavMeshAgent navMeshAgent;

        // FSM states
        private MoveToState moveToState;
        private AlignToState alignToState;
        private IdleState idleState;
        private AttackState stompState;
        private ThrowingRocksFSM throwingRocksState;

        private EnemyController enemyController;
        
        public BossFSM(NavMeshAgent pNavMeshAgent, Blackboard pBlackboard)
        {
            Debug.Log("Initialize BossFSM");
            
            navMeshAgent = pNavMeshAgent;
            
            blackboard = pBlackboard;

            enemyController = blackboard.stateOwnerTransform.GetComponent<EnemyController>();

            //Create states
            moveToState = new MoveToState(navMeshAgent, blackboard);
            alignToState = new AlignToState(blackboard);
            idleState = new IdleState(blackboard);
            stompState = new AttackState(blackboard, Tags.Player);
            throwingRocksState = new ThrowingRocksFSM(navMeshAgent, blackboard, 20);

            //Transitions setup

            //While idling:
            //if a target is in range, transition to moveToState to chase the target.
            idleState.transitions.Add(new Transition(() => idleState.IsTargetInRange() && idleState.IdleTimeOver(), moveToState));

            //While moving to the target:
            //1. If the target is reached, transition to alignToState to align to the target.
            //2. If the target is out of range, transition to idle state.
            moveToState.transitions.Add(new Transition(moveToState.TargetReached, alignToState));
            moveToState.transitions.Add(new Transition(moveToState.TargetOutOfRange, idleState));

            //While aligning to the target:
            //If the target is out of range, transition to moveToState to chase the target again.
            alignToState.transitions.Add(new Transition(alignToState.TargetOutOfRange, moveToState));
            alignToState.transitions.Add(new Transition(alignToState.AlignedWithTarget, stompState));

            //When attack is over:
            //Re-align to target
            stompState.transitions.Add(new Transition(stompState.FinishedAttacking, throwingRocksState));
            
            throwingRocksState.transitions.Add(new Transition(throwingRocksState.BehaviourDurationExceeded, idleState));


            // ANIMATION TRIGGERS CAN BE ADDED HERE
            idleState.onEnter += () => { };
            idleState.onExit += () => { };
            moveToState.onEnter += () => { };
            moveToState.onExit += () => { };
            alignToState.onEnter += () => { };
            alignToState.onExit += () => { };
            stompState.onEnter += () => { };
            stompState.onExit += () => { };
            throwingRocksState.onEnter += () => { };
            throwingRocksState.onExit += () => { };

            throwingRocksState.onEnter += () =>
            {
                enemyController.SwitchAttackBehaviour(typeof(RangedAttackBehaviour));
                enemyController.SwitchAttackData(blackboard.bossDefaultAttackData);
            };
            throwingRocksState.onExit += () =>
            {
                enemyController.SwitchAttackBehaviour(typeof(BossAoEAttackBehaviour));
                enemyController.SwitchAttackData(blackboard.bossSlamAttackData);
            };

            //Default state is idleState.
            currentState = idleState;
        }
        
        public override void Enter()
        {
            base.Enter();
            currentState.Enter();
        }

        //In each update, step the current state to let it process, then check if a condition is met
        //for one of the current state's transitions, if so transition to next state.
        /*void Update()
        {
            Step();
            blackboard.currentState = currentState.stateName;
            blackboard.stateText.text = currentState.stateName;
        }*/
    }
}
