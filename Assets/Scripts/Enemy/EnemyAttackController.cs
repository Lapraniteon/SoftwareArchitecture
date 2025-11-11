using System.Collections;
using UnityEngine;

public class EnemyAttackController : EnemyObserver
{

    private PlayerModel playerModel;

    private Coroutine attackCoroutine; // Coroutine used to manage attack timing
    private bool attacking = false; // Indicates whether the enemy is currently attacking

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
        StopCoroutine(attackCoroutine);
        attackCoroutine = null;
        attacking = false;
    }
    
    private IEnumerator Attack()
    {
        while (attacking)
        {
            yield return new WaitForSeconds(0.5f);

            // Get the current target from the selector
            if (playerModel == null)
            {
                Debug.LogWarning("Enemy attacks but has no player to attack");
                continue;
            }
            
            playerModel.GetHit(1);
        }
    }
    
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
    
    protected override void OnEnemyCreated(Enemy enemy)
    {
        //
    }

    protected override void OnEnemyHit(Enemy enemy)
    {
        //
    }
}
