using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

public class MouseClickController : MonoBehaviour
{
    public Vector3 clickPosition;
    private Vector3 rayOrigin;

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
                
                // Trigger an unity event to notify other scripts about the click here
                onRaycastHit.Invoke(clickPosition);
            } 
        } 
        
        // Add visual debugging here
        Debug.DrawLine(rayOrigin, clickPosition, Color.yellow);
    } 

}
