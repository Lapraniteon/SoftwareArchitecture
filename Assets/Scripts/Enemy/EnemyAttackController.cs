using System.Collections;
using Unity.Collections;
using UnityEngine;

public class EnemyAttackController : EnemyObserver
{

    PlayerModel playerModel;

    private Coroutine attackCoroutine; // Coroutine used to manage attack timing
    private bool attacking = false; // Indicates whether the enemy is currently attacking

    [SerializeField] 
    private AttackData attackData;

    [SerializeField]
    private EnemyAttackBehaviour attackBehaviour;
    
    private void Start()
    {
        playerModel = GameManager.Instance.playerModel;

        attackBehaviour = GetComponent<EnemyAttackBehaviour>();
    }
    
    private void StartAttacking()
    {
        attacking = true;
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(Attack());
        }
    }

    private void StopAttacking()
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
            yield return new WaitForSeconds(attackData.attackInterval);

            // Get the current target from the selector
            /*if (playerModel == null)
            {
                Debug.LogWarning("Enemy attacks but has no player to attack");
                continue;
            }
            
            playerModel.GetHit(attackData.damage);*/

            attackBehaviour?.Attack(playerModel, attackData);
        }
    }
    
    
    #region Trigger-based range checking code
    
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
    
    #endregion
}
