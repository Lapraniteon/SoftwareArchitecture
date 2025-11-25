using System.Collections;
using SADungeon.Player;
using Unity.Collections;
using UnityEngine;

namespace SADungeon.Enemy
{

    public class AttackController : EnemyObserver
    {

        private PlayerModel playerModel;

        private Coroutine attackCoroutine; // Coroutine used to manage attack timing
        private bool attacking = false; // Indicates whether the enemy is currently attacking
        private bool waiting = false;

        [SerializeField] private AttackData attackData;

        private AttackBehaviour attackBehaviour;

        private void Start()
        {
            playerModel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerModel>();

            attackBehaviour = GetComponent<AttackBehaviour>();
            if (attackBehaviour is null)
                Debug.LogWarning($"No attack behaviour attached to {gameObject.name}!");
        }

        public void StartAttacking()
        {
            attacking = true;
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(Attack());
            }
        }

        public void StopAttacking()
        {
            if (attackCoroutine != null)
                StopCoroutine(attackCoroutine);

            attackCoroutine = null;
            attacking = false;
        }

        private IEnumerator Attack()
        {
            while (attacking)
            {
                waiting = true;
                yield return new WaitForSeconds(attackData.attackInterval);
                waiting = false;

                // Get the current target from the selector
                /*if (playerModel == null)
                {
                    Debug.LogWarning("Enemy attacks but has no player to attack");
                    continue;
                }

                playerModel.GetHit(attackData.damage);*/

                attackBehaviour?.Attack(playerModel.transform, attackData, Tags.Player);
            }
        }

        public bool IsWaitingForAttack()
        {
            return waiting;
        }

        public float GetAttackRange()
        {
            return attackBehaviour?.attackRange ?? 0f;
        }


        /*#region Trigger-based range checking code

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (playerModel == null)
                    playerModel = other.GetComponent<PlayerModel>();

                enemyController.playerInAttackRange = true;

                StartAttacking();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                enemyController.playerInAttackRange = false;

                StopAttacking();
            }
        }

        #endregion*/
    }
}
