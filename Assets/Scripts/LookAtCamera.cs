using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// Helper script to make enemy health displays (mostly) face the camera.
/// </summary>

[RequireComponent(typeof(LookAtConstraint))]
public class LookAtCamera : MonoBehaviour
{
    private LookAtConstraint constraint;

    void Awake()
    {
        constraint = GetComponent<LookAtConstraint>();

        ConstraintSource source = new();
        source.sourceTransform = Camera.main.transform;
        source.weight = 1f;
        constraint.SetSource(0, source);
    }
}
