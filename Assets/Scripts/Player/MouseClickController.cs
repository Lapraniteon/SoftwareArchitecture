using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles mouse clicks and converts it into a point in world space for the player NavMeshAgent to navigate to.
/// </summary>

public class MouseClickController : MonoBehaviour
{
    public Vector3 clickPosition;
    private Vector3 rayOrigin;

    [Tooltip("The VFX to spawn at the click point.")]
    [SerializeField] private ParticleSystem clickIndicatorVFX;

    public UnityEvent<Vector3> onRaycastHit;
    
    void Update() { 
        // Get the mouse click position in world space 
        if (Input.GetMouseButtonDown(0)) { 
            Ray mouseRay = Camera.main.ScreenPointToRay( Input.mousePosition ); 
            if (Physics.Raycast( mouseRay, out RaycastHit hitInfo )) { 
                Vector3 clickWorldPosition = hitInfo.point; 
                Debug.Log(clickWorldPosition); 
                
                clickPosition = clickWorldPosition;
                rayOrigin = mouseRay.origin;
                
                Instantiate(clickIndicatorVFX, clickPosition, Quaternion.identity);
                
                // Trigger an unity event to notify other scripts about the click here
                onRaycastHit.Invoke(clickPosition);
            } 
        } 
        
        // Add visual debugging here
        Debug.DrawLine(rayOrigin, clickPosition, Color.yellow);
    } 

}
