using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]

public class UIClick_XR : MonoBehaviour
{
    //private XRBaseInteractable interactable;
    public GameObject objToHide;
    public GameObject objToShow;
    public bool LoadScene;
    public string sceneToLoad;
    public void CustomFunction()
    {
        Debug.Log("executing custom function!");
        objToShow.SetActive(true);
        objToHide.SetActive(false);
        SceneManager.LoadScene(sceneToLoad);
    }
}