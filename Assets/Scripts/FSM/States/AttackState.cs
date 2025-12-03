using System.Collections;
using SADungeon.Enemy;
using SADungeon.Player;
using UnityEngine;
using UnityEngine.Rendering;

namespace SADungeon.FSM
{

    /// <summary>
    /// A dummy attack state
    /// </summary>
    public class AttackState : State
    {
        private Transform target;
        private AttackData attackData;
        private AttackBehaviour attackBehaviour;
        private string targetTag;
        private float attackInterval;

        private float attackStartTime;

        public AttackState(Blackboard pBlackboard, string pTargetTag)
        {
            target = pBlackboard.target;
            attackData = pBlackboard.attackData;
            attackBehaviour = pBlackboard.attackBehaviour;
            targetTag = pTargetTag;
            attackInterval = pBlackboard.attackInterval;

            stateName = "Attack";
        }

        public override void Enter()
        {
            base.Enter();
            attackStartTime = Time.time;
            attackBehaviour?.Attack(target, attackData, targetTag);
        }
        
        public bool FinishedAttacking()
        {
            return Time.time > attackStartTime + attackInterval;
        }
    }
}