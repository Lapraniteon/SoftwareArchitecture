using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A manager script that handles global input events.
/// Unity's InputSystem does this way better, this is a basic implementation for this specific purpose.
/// </summary>

public class GlobalInputManager : MonoBehaviour
{
    
    private bool _inventoryEnabled;
    public UnityEvent onInventoryEnable = new();
    public UnityEvent onInventoryDisable = new();

    /// <summary>
    /// Fires when the heal button is pressed.
    /// </summary>
    public static event Action onHealButtonPressed;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_inventoryEnabled)
            {
                _inventoryEnabled = false;
                onInventoryDisable.Invoke();
            }
            else
            {
                _inventoryEnabled = true;
                onInventoryEnable.Invoke();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.H))
            onHealButtonPressed?.Invoke();
    }
}
