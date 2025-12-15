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

        private float attackStartTime;

        public AttackState(Blackboard pBlackboard, string pTargetTag)
        {
            blackboard = pBlackboard;
            target = pBlackboard.target;
            //attackData = pBlackboard.currentAttackData;
            //attackBehaviour = pBlackboard.attackBehaviour;
            targetTag = pTargetTag;

            stateName = "Attack";
        }

        public override void Enter()
        {
            base.Enter();
            attackStartTime = Time.time;
            blackboard.attackBehaviour?.Attack(blackboard.stateOwnerTransform, target, blackboard.currentAttackData, targetTag);
        }
        
        public bool FinishedAttacking()
        {
            return Time.time > attackStartTime + blackboard.currentAttackData.attackInterval;
        }
    }
}