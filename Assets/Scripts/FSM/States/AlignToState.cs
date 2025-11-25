using UnityEngine;

namespace SADungeon.FSM
{

    /// <summary>
    /// A state that rotates a Transform to align to the target transform.
    /// </summary>
    public class AlignToState : State
    {
        private Transform target;
        private Transform self;
        private Vector3 direction;
        private float rotationSpeed;
        private float rotationSign;
        private float targetRange;

        public AlignToState(Transform pSelf, Transform pTarget, float pRotationSpeed, float pTargetRange)
        {
            self = pSelf;
            SetTarget(pTarget);
            rotationSpeed = pRotationSpeed;
            targetRange = pTargetRange;

            stateName = "AlignTo";
        }

        public void SetTarget(Transform pTarget)
        {
            target = pTarget;
        }


        public override void Step()
        {
            base.Step();

            // Compute the normalized direction vector toward the target
            direction = (target.position - self.position).normalized;

            // Determine whether to rotate clockwise or counterclockwise
            rotationSign = Mathf.Sign(Vector3.Dot(self.right, direction));

            // Rotate around the up axis based on direction and speed
            self.Rotate(self.up, rotationSign * rotationSpeed * Time.deltaTime);
        }

        //--------Helper Condition for Transitions to decide whether to transition to the next state
        public bool AlignedWithTarget()
        {
            return Vector3.Dot(self.forward, direction) >= 0.95f;
        }

        public bool TargetOutOfRange()
        {
            return Vector3.Distance(self.position, target.position) > targetRange;
        }
    }
}