using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CostData : MonoBehaviour
{
    [Header("ObjectsInSceneList")]
    public List<ObjectInformation> ObjectsInScene = new List<ObjectInformation>();
    [Header("UI Reference")]
    public GameObject CanvasGameObj;
    public TextMeshProUGUI TextBox;
    bool UI_On = true;
    [Header("TriggerAction")]
    public InputActionReference triggerAction;
    void Start()
    {
        if (TextBox == null)
        {
            Debug.LogWarning("You Forgot to link a textbox to Cost Data!");
            //GameObject obj = GameObject.FindWithTag("ObjectsInSceneUITag");
            //if (obj != null)
            //{
            //    TextBox = obj.GetComponent<TextMeshProUGUI>();
            //    if (TextBox == null) { Debug.LogWarning("Couldn't find the TMPro component on " + obj.name); }
            //}
            //else { Debug.LogWarning("Couldn't find a GameObject with tag ObjectsInSceneUITag"); }
        }
        if (CanvasGameObj == null) { Debug.LogWarning("You forgot to link a canvas GameObject to Cost Data!"); }
    }
    void OnEnable()
    {
        if (triggerAction == null) { Debug.LogError("Trigger Action not assigned!"); return; }

        triggerAction.action.performed += OnTriggerPressed;
        triggerAction.action.Enable();
    }

    private void OnTriggerPressed(InputAction.CallbackContext context)
    {
        UI_On = !UI_On;
        CanvasGameObj.SetActive(UI_On);
        ShowCurrentList();
    }
    public void ShowCurrentList()
    {
        int totalcost = 0;
        string textBuffer = "";
        foreach (ObjectInformation objInfo in ObjectsInScene)
        {
            textBuffer += "- " + objInfo.CustomName + ", Texture: " + objInfo.currentTextureName +
                ", Cost: " + objInfo.currentCost + "\n";
            totalcost += objInfo.currentCost;
        }
        if (textBuffer == "")
        {
            Debug.LogWarning("Attempting to output text but empty.");
            return;
        }
        textBuffer += "\n TOTAL CURRENT COST: " + totalcost;
        //Debug.Log(textBuffer);
        TextBox.text = textBuffer;
    }
    private void OnDisable()
    {
        triggerAction.action.performed -= OnTriggerPressed;
        triggerAction.action.Disable();
    }
}