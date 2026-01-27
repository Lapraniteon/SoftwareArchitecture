using SADungeon.Enemy;
using UnityEngine;

namespace SADungeon.Player
{
    
    /// <summary>
    /// Class responsible for calling the attack method of the attached attackBehaviour.
    /// </summary>
    
    [RequireComponent(typeof(EnemyTargetSelector))]
    public class AttackController : MonoBehaviour
    {

        private EnemyTargetSelector targetSelector;

        private EnemyController currentTarget;

        [Tooltip("The AttackData to apply to the attack.")]
        [SerializeField] private AttackData attackData;

        private AttackBehaviour attackBehaviour;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            targetSelector = GetComponent<EnemyTargetSelector>();

            attackBehaviour = GetComponent<AttackBehaviour>();
            if (attackBehaviour is null)
                Debug.LogWarning($"No attack behaviour attached to {gameObject.name}!");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentTarget = targetSelector.GetTarget();

                if (currentTarget is not null)
                    attackBehaviour.Attack(transform, currentTarget.transform, attackData, Tags.Enemy);
            }
        }
    }
}