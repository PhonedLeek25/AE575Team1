using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

//[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable))]
[RequireComponent(typeof(Collider))]
public class UIClick_XR : MonoBehaviour
{
    //private XRBaseInteractable interactable;
    public GameObject objToHide;
    public GameObject objToShow;

    //void Awake()
    //{
    //    interactable = GetComponent<XRBaseInteractable>();
    //
    //    if (!TryGetComponent(out Collider col))
    //    {
    //        Debug.LogError("No Collider found on " + gameObject.name);
    //    }
    //
    //    interactable.selectEntered.AddListener(OnClicked);
    //}
    //void OnDestroy()
    //{
    //    if (interactable != null)
    //    {
    //        interactable.selectEntered.RemoveListener(OnClicked);
    //    }
    //}
    //void OnClicked(SelectEnterEventArgs args)
    //{
    //    Debug.Log("Object clicked: " + gameObject.name);
    //
    //    CustomFunction();
    //}
    public void CustomFunction()
    {
        Debug.Log("executing custom function!");
        objToShow.SetActive(true);
        objToHide.SetActive(false);
    }
}