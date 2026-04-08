using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//public class ObjectInfo
//{
//    public string Name;
//    //public string textureName;
//    public Texture texture;
//    public int TextureCostPerUnit;
//    public int CurrentCost;
//    public ObjectInfo(string Name, Texture texture, int TextureCostPerUnit, int CurrentCost)
//    {
//        this.Name = Name;
//        this.texture = texture;
//        this.TextureCostPerUnit = TextureCostPerUnit;
//        this.CurrentCost = CurrentCost;
//    }
//}

//[SerializeField]
//public List<ObjectInfo> ObjectsInScene = new List<ObjectInfo>();
//public void AddItem(string Name, Texture texture = null, int TextureCostPerUnit = 0, int CurrentCost = 0)
//{
//    ObjectsInScene.Add(new ObjectInfo(Name, texture, TextureCostPerUnit, CurrentCost));
//}


public class CostData : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI TextBox;
    public List<ObjectInformation> ObjectsInScene = new List<ObjectInformation>();
    public int ObjectCount = 0;
    private void Start()
    {
        
    }
    public void ShowCurrentList()
    {
        string textBuffer = "NothingAssigned";
        foreach (ObjectInformation objInfo in ObjectsInScene)
        {
            //Debug.Log("Object Name: " + objInfo.CustomName + ", Texture: " + objInfo.currentTextureName + ", Cost: " + objInfo.currentCost);
            textBuffer = "Object Name: " + objInfo.CustomName + ", Texture: " + objInfo.currentTextureName + ", Cost: " + objInfo.currentCost;
        }
        if (textBuffer == "NothingAssigned")
        {
            Debug.LogWarning("Attempting to output text but empty.");
            return;
        }
        TextBox.text = textBuffer;
    }
    public void AddObjCostData(ObjectInformation objInfo)
    {
        ObjectsInScene.Add(objInfo); ObjectCount++;
    }
}