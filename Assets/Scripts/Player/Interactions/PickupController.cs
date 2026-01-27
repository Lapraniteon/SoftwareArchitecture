using SADungeon.Player;
using UnityEngine;

/// <summary>
/// Class responsible for processing player input to pick up items.
/// </summary>

[RequireComponent(typeof(PickupTargetSelector))]
public class PickupController : MonoBehaviour
{

    private PickupTargetSelector targetSelector;

    private GroundPickup currentTarget;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetSelector = GetComponent<PickupTargetSelector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentTarget = targetSelector.GetTarget();
            currentTarget?.PickupItem(); // If there is any target in range, pick it up.
        }
    }
}
