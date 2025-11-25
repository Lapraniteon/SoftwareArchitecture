using UnityEngine;

namespace SADungeon.FSM
{

    /// <summary>
    /// A state to idle for a period of time(idleTime).
    /// </summary>
    public class IdleState : State
    {
        private float detectRange;
        private Transform self;
        private Transform target;
        private float idleTime;
        private float startTime;

        public IdleState(float pDetectRange, Transform pSelf, Transform pTarget, float pIdleTime)
        {
            detectRange = pDetectRange;
            self = pSelf;
            target = pTarget;
            idleTime = pIdleTime;

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
            return (Vector3.Distance(self.transform.position, target.transform.position) <= detectRange);
        }

        public bool IdleTimeOver()
        {
            return Time.time > startTime + idleTime;
        }
    }
}