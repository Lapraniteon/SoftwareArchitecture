using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{

    [SerializeField] public float attackRange = 0;
    
    public abstract void Attack(Transform target, AttackData attackData, string targetTag);
}
