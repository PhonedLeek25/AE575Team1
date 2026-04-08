using UnityEngine;
using UnityEngine.InputSystem;

public class ObjRemoteController : MonoBehaviour
{
    public ObjectInformation TargetObject;
    //public GameObject TargetGameObject;
    [Header("Ray Settings")]
    public float rayDistance = 10f;
    //[SerializeField] private LayerMask interactableLayer = ~0; //all layers
    public LayerMask interactableLayer = LayerMask.GetMask("InteractableRaycast"); //only detect certain layer.
    [SerializeField] private bool debugRay = true;

    [Header("Input Interaction Action Linker")]
    [SerializeField] private InputActionProperty interactAction;

    [Header("Current Target (Debug)")]
    [SerializeField] private ObjectInformation ObjInfoScript;
    [SerializeField] private RaycastHit currentHit;

    private void Update()
    {
        DetectTarget();

        if (ObjInfoScript != null)
        {
            //highlight
        }

        if (InteractPressed())
        {
            Interaction();
        }
    }

    private void DetectTarget()
    {
        ObjInfoScript = null; //reset target
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out currentHit, rayDistance, interactableLayer))
        {
            // First try on the object directly hit
            ObjInfoScript = currentHit.collider.GetComponent<ObjectInformation>();
            // If collider is on a child, try parent too
            if (ObjInfoScript == null)
            {
                ObjInfoScript = currentHit.collider.GetComponentInParent<ObjectInformation>();
            }
        }
    }

    private bool InteractPressed() //checks if interactAction.action was pressed and isn't invalid.
    {
        return interactAction.action != null && interactAction.action.WasPressedThisFrame();
    }

    private void Interaction()
    {
        //Failsafe
        if (ObjInfoScript == null)
        {
            Debug.Log("Attempted to interact, but ObjInfoScript is unassigned.");
            return;
        }

        //Get Info! :D
        //ObjInfoScript.textureA
        
    }

    private void OnDrawGizmos() //shows ray! :)
    {
        if (!debugRay) { return; }
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }

    public ObjectInformation GetCurrentTarget()
    {
        return ObjInfoScript;
    }
}