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
    public class EnemyFSM : MonoBehaviour
    {
        private Transform target;
        private NavMeshAgent navMeshAgent;
        [SerializeField] private float chaseRange = 3f;
        [SerializeField] private float chaseThreshold = 1f;
        private float attackRange;
        [SerializeField] private float rotateSpeed = 90f;
        [SerializeField] private float idleTime = 2f;
        private AttackController attackController;

        // FSM states
        private MoveToState moveToState;
        private AlignToState alignToState;
        private IdleState idleState;
        private AttackState attackState;

        // The current state
        [SerializeReference] private State currentState;

        [SerializeField] private TextMeshProUGUI stateText;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            
            target = GameObject.FindGameObjectWithTag("Player").transform;

            attackController = GetComponent<AttackController>();

            attackRange = attackController.GetAttackRange();

            //Create states
            moveToState = new MoveToState(target, navMeshAgent, chaseThreshold, chaseRange);
            alignToState = new AlignToState(transform, target, rotateSpeed, chaseRange);
            idleState = new IdleState(chaseRange, transform, target, idleTime);
            attackState = new AttackState(attackController);

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

        //In each update, step the current state to let it process, then check if a condition is met
        //for one of the current state's transitions, if so transition to next state.
        void Update()
        {
            currentState.Step();
            if (currentState.NextState() != null)
            {
                //Cache the next state, because after currentState.Exit, calling
                //currentState.NextState again might return null because of change
                //of context.
                State nextState = currentState.NextState();
                currentState.Exit();
                currentState = nextState;
                stateText.text = currentState.stateName;
                currentState.Enter();
            }
        }
    }
}
