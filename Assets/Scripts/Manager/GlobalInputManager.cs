using System;
using UnityEngine;
using UnityEngine.Events;

public class GlobalInputManager : MonoBehaviour
{
    
    private bool _inventoryEnabled;
    public UnityEvent onInventoryEnable = new();
    public UnityEvent onInventoryDisable = new();

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
