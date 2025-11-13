using UnityEngine;

[RequireComponent(typeof(EnemyTargetSelector))]
public class AttackController : MonoBehaviour
{

    private EnemyTargetSelector targetSelector;
    
    private EnemyController currentTarget;

    [SerializeField]
    private AttackData attackData;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetSelector = GetComponent<EnemyTargetSelector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentTarget = targetSelector.GetTarget();
            currentTarget?.GetHit(attackData);
        }
    }
}
