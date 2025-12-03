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
    public class EnemyFSM : FSM
    {
        private NavMeshAgent navMeshAgent;

        // FSM states
        private MoveToState moveToState;
        private AlignToState alignToState;
        private IdleState idleState;
        private AttackState attackState;
        
        public EnemyFSM(NavMeshAgent pNavMeshAgent, Blackboard pBlackboard)
        {
            Debug.Log("Initialize enemyFSM");
            
            navMeshAgent = pNavMeshAgent;
            
            blackboard = pBlackboard;

            //Create states
            moveToState = new MoveToState(navMeshAgent, blackboard);
            alignToState = new AlignToState(blackboard);
            idleState = new IdleState(blackboard);
            attackState = new AttackState(blackboard);

            //Transitions setup

            //While idling:
            //if a target is in range, transition to moveToState to chase the target.
            idleState.transitions.Add(new Transition(idleState.IsTargetInRange, moveToState));

            //While moving to the target:
            //1. If the target is reached, transition to alignToState to align to the target.
            //2. If the target is out of range, transition to idle state.
            moveToState.transitions.Add(new Transition(moveToState.TargetReached, alignToState));
            moveToState.transitions.Add(new Transition(moveToState.TargetOutOfRange, idleState));

            //While aligning to the target:
            //If the target is out of range, transition to moveToState to chase the target again.
            alignToState.transitions.Add(new Transition(alignToState.TargetOutOfRange, moveToState));
            alignToState.transitions.Add(new Transition(alignToState.AlignedWithTarget, attackState));

            //When attack is over:
            //Re-align to target
            attackState.transitions.Add(new Transition(attackState.AttackOver, alignToState));


            /*idleState.onEnter += () => { animator.SetBool("Idle", true); };
            idleState.onExit += () => { animator.SetBool("Idle", false); };
            moveToState.onEnter += () => { animator.SetBool("Chase", true); };
            moveToState.onExit += () => { animator.SetBool("Chase", false); };
            alignToState.onEnter += () => { animator.SetBool("Aim", true); };
            alignToState.onExit += () => { animator.SetBool("Aim", false); };
            attackState.onEnter += () => { animator.SetBool("Attack", true); };
            attackState.onExit += () => { animator.SetBool("Attack", false); };*/

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
