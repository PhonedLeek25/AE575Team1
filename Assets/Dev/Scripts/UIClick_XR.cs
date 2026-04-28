using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]

public class UIClick_XR : MonoBehaviour
{
    //private XRBaseInteractable interactable;
    public GameObject objToHide;
    public GameObject objToShow;
    public bool LoadScene;
    public int sceneIndex;
    public void CustomFunction()
    {
        if (!LoadScene)
        {
            Debug.Log("executing custom function!");
            objToShow.SetActive(true);
            objToHide.SetActive(false);
        }
        else
        {
            Debug.Log("Loading New Scene!");
            SceneManager.LoadScene(sceneIndex);
        }
    }
}