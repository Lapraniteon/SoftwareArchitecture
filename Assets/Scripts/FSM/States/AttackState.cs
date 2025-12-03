using SADungeon.Enemy;
using UnityEngine;
using UnityEngine.Rendering;

namespace SADungeon.FSM
{

    /// <summary>
    /// A dummy attack state
    /// </summary>
    public class AttackState : State
    {
        private AttackController _attackController;

        public AttackState(Blackboard pBlackboard)
        {
            _attackController = pBlackboard.attackController;

            stateName = "Attack";
        }

        public override void Enter()
        {
            base.Enter();
            _attackController.StartAttacking();
        }

        public override void Exit()
        {
            base.Exit();
            _attackController.StopAttacking();
        }

        //--------Helper Condition for Transitions to decide whether to transition to the next state
        public bool AttackOver()
        {
            return _attackController.IsWaitingForAttack();
        }
    }
}