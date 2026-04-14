using UnityEngine;
using UnityEngine.XR;

public class RaySelector : MonoBehaviour
{
    public UnityEngine.InputSystem.InputActionReference triggerAction;
    public float rayLength = 10f;
    public LayerMask selectableLayers;

    private ObjectInformation currentHit;
    private bool triggerWasPressed = false;

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.red);

        bool triggerPressed = triggerAction != null && triggerAction.action.WasPressedThisFrame();
        if (triggerPressed)
            Debug.Log("Trigger is pressed!");

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward,
            out hit, rayLength, selectableLayers))
        {
            Debug.Log("Ray hit: " + hit.collider.gameObject.name + " on layer: " + hit.collider.gameObject.layer);
            ObjectInformation objInfo = hit.collider.GetComponentInParent<ObjectInformation>();
            if (objInfo != null)
            {
                currentHit = objInfo;
                if (triggerPressed && !triggerWasPressed)
                {
                    Debug.Log("Trigger pressed on: " + objInfo.CustomName);
                    currentHit.AlexActivateMePleaseThankYouOBJINFO();
                }
            }
            else
            {
                Debug.Log("No ObjectInformation found on hit object!");
            }
        }

        triggerWasPressed = triggerPressed;
    }
}