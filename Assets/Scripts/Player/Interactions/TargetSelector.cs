using System.Collections.Generic;
using UnityEngine;

namespace SADungeon.Player
{
    
    /// <summary>
    /// Abstract class for different types of target selectors.
    /// </summary>
    
    public abstract class TargetSelector : MonoBehaviour
    {
        [Tooltip("The targets currently in range.")]
        [SerializeField] protected List<Transform> targetsInRange = new();
    }
}