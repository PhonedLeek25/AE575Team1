using UnityEngine;
using UnityEngine.XR;

public class RaySelector : MonoBehaviour
{
    public UnityEngine.InputSystem.InputActionReference triggerAction;
    public float rayLength = 10f;
    public LayerMask selectableLayers;

    private ObjectInformation currentObjInfoScript;

    void Start()
    {
        if (selectableLayers == 0) { selectableLayers = LayerMask.GetMask("InteractableRaycast"); }
    }
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.red);

        bool triggerPressed = triggerAction != null && triggerAction.action.WasPressedThisFrame();

        RaycastHit hit;
        bool didHit = Physics.Raycast(transform.position, transform.forward,
            out hit, rayLength, selectableLayers);

        if (triggerPressed)
        {
            if (didHit)
            {
                ObjectInformation objInfoScript = hit.collider.GetComponentInParent<ObjectInformation>();

                if (objInfoScript != null)
                {
                    Debug.Log("Trigger pressed on: " + objInfoScript.CustomName);

                    // Open new UI
                    objInfoScript.ActivateUI();

                    // Track current
                    currentObjInfoScript = objInfoScript;
                }
                else
                {
                    CloseCurrentUI();
                }
            }
            else
            {
                // Clicked empty space
                CloseCurrentUI();
            }
        }
    }
    void CloseCurrentUI()
    {
        if (currentObjInfoScript != null)
        {
            currentObjInfoScript.DeactivateUI(); // you need this function
            currentObjInfoScript = null;
            Debug.Log("Closed UI");
        }
    }
}