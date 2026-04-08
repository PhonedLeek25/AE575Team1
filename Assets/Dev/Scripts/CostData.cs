using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum TextureTypes
{
    None, Wall, Ceiling, Floor, FFE
}
public enum CostUnits
{
    None, SF, LF, EA
}

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
    public ObjectInformation ObjInfoRef;
    private void Start()
    {
        ObjInfoRef.changeTexture(ObjectInformation.option.A);
    }
}