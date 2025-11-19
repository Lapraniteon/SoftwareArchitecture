using System.Collections.Generic;
using UnityEngine;

namespace SADungeon.Player
{
    public class TargetSelector : MonoBehaviour
    {
        [SerializeField] protected List<Transform> targetsInRange = new();
    }
}