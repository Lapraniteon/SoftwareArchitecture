using UnityEngine;
using UnityEngine.Events;

public class GlobalInputManager : MonoBehaviour
{
    
    private bool _inventoryEnabled;
    public UnityEvent onInventoryEnable = new();
    public UnityEvent onInventoryDisable = new();
    
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
    }
}
