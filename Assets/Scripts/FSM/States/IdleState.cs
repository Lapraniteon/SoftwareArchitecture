using UnityEngine;

namespace SADungeon.FSM
{

    /// <summary>
    /// A state to idle for a period of time(idleTime).
    /// </summary>
    public class IdleState : State
    {
        private float chaseRange;
        private Transform self;
        private Transform target;
        private float idleTime;
        private float startTime;

        public IdleState(Blackboard pBlackboard)
        {
            chaseRange = pBlackboard.chaseRange;
            self = pBlackboard.stateOwnerTransform;
            target = pBlackboard.target;
            idleTime = pBlackboard.idleTime;

            stateName = "Idle";
        }

        public override void Enter()
        {
            base.Enter();
            startTime = Time.time;
        }

        //--------Helper Conditions for Transitions to decide whether to transition to the next state

        public bool IsTargetInRange()
        {
            if (target == null)
                return false;
            
            return (Vector3.Distance(self.transform.position, target.transform.position) <= chaseRange);
        }

        public bool IdleTimeOver()
        {
            return Time.time > startTime + idleTime;
        }
    }
}