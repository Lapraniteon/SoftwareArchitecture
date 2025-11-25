using UnityEngine;
using UnityEngine.AI;

namespace SADungeon.FSM
{

    /// <summary>
    /// This state uses NavMeshAgent to move to toward a target transform.
    /// </summary>
    public class MoveToState : State
    {
        private Transform target;
        private NavMeshAgent navMeshAgent;
        private float distanceThreshold;
        private float targetRange;

        public MoveToState(Transform pTarget, NavMeshAgent pNavMeshAgent, float pDistanceThreshold, float pTargetRange)
        {
            target = pTarget;
            navMeshAgent = pNavMeshAgent;
            distanceThreshold = pDistanceThreshold;
            targetRange = pTargetRange;

            stateName = "MoveTo";
        }

        //When entering the state, enable navMeshAgent to start moving.
        public override void Enter()
        {
            base.Enter();
            navMeshAgent.enabled = true;
        }

        public override void Step()
        {
            navMeshAgent.SetDestination(target.position);
            base.Step();
        }

        //When exitting the state, disable navMeshAgent otherwise it will keep moving.
        public override void Exit()
        {
            base.Exit();
            navMeshAgent.enabled = false;
        }


        //--------Helper Conditions for Transitions to decide whether to transition to the next state
        public bool TargetReached()
        {
            return Vector3.Distance(navMeshAgent.transform.position, target.position) <= distanceThreshold;
        }

        //Can be used as a condition for transitions to check to see if it's time to transition
        //to the next state.
        public bool TargetOutOfRange()
        {
            return Vector3.Distance(navMeshAgent.transform.position, target.position) > targetRange;
        }
    }
}