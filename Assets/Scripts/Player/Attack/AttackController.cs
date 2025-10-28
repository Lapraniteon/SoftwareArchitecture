using UnityEngine;

public class AttackController : MonoBehaviour
{

    [SerializeField] private EnemyController currentTarget;
    
    [SerializeField] private TargetSelector targetSelector;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentTarget = targetSelector.GetTarget();
            currentTarget?.GetHit();
        }
    }
}
