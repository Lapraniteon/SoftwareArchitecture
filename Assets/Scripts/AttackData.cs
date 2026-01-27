using UnityEngine;

/// <summary>
/// A ScriptableObject that stores an attack's damage, interval and the VFX to spawn when the attack hits.
/// </summary>

[CreateAssetMenu(fileName = "AttackData", menuName = "Scriptable Objects/AttackData")]
public class AttackData : ScriptableObject
{
    public int damage;
    public float attackInterval;
    
    [Header("VFX")]
    public ParticleSystem vfx;
}
