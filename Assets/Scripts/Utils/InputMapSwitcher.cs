using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class switches between different action maps for the input system
/// when a component is enabled/disabled.
/// </summary>
public class InputMapSwitcher : MonoBehaviour
{
    [SerializeField]
    private string SwitchToMapOnEnable;
    [SerializeField]
    private string SwitchToMapOnDisable;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = FindAnyObjectByType<PlayerInput>();
    }
    private void OnEnable()
    {
        if(playerInput != null)
        {
            playerInput.SwitchCurrentActionMap(SwitchToMapOnEnable);
        }   
    }

    private void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.SwitchCurrentActionMap(SwitchToMapOnDisable);
        }
    }
}
