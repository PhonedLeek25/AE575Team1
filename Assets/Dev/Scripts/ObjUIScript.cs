using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjUIScript : MonoBehaviour
{
    [Header("Permanent Link")]
    public ObjectInformation ObjInfoScript;
    public TMP_Dropdown dropdown;

    [Space(10)]
    [Header("UI Preset (Private) Links")]
    [SerializeField] private TextMeshProUGUI ObjectName;
    [SerializeField] private TextMeshProUGUI CurrentSelection;
    [SerializeField] private string CurrentCost;
    [SerializeField] private Image currentImage;
    //[Space(10)]
    //[SerializeField] private TextMeshProUGUI optionAlabel;
    //[SerializeField] private string optionAcost;
    //[SerializeField] private Image optionAImage;
    //[Space(10)]
    //[SerializeField] private TextMeshProUGUI optionBlabel;
    //[SerializeField] private string optionBcost;
    //[SerializeField] private Image optionBImage;
    //[Space(10)]
    //[SerializeField] private TextMeshProUGUI optionClabel;
    //[SerializeField] private string optionCcost;
    //[SerializeField] private Image optionCImage;


    void Start()
    {
        if (ObjInfoScript == null)
        {
            ObjInfoScript = gameObject.GetComponentInParent<ObjectInformation>();
            if (ObjInfoScript == null)
            {
                Debug.LogWarning("ObjUIScript unable to fetch ObjInfoScript on parent(s)\nExiting.");
                this.enabled = false; return;
            }
        }
        if (dropdown == null)
        {
            Debug.LogWarning("Fatal Error: ObjUIScript unable to fetch dropdown component!\nExiting.");
            this.enabled = false; return;
        }
        LinkItems();
        dropdown.onValueChanged.AddListener(DropdownListener);
    }
    void LinkItems()
    {
        ObjectName.text = ObjInfoScript.CustomName;
        CurrentSelection.text = ObjInfoScript.currentTextureName + " ($" + ObjInfoScript.currentCost.ToString() + ")";


        //Update Dropdown options
        string DropDownLabelA = ObjInfoScript.textureAName + " ($" + ObjInfoScript.costA.ToString() + ")";
        string DropDownLabelB = ObjInfoScript.textureBName + " ($" + ObjInfoScript.costB.ToString() + ")";dropdown.ClearOptions();
        string DropDownLabelC = ObjInfoScript.textureCName + " ($" + ObjInfoScript.costC.ToString() + ")";
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        options.Add(new TMP_Dropdown.OptionData(DropDownLabelA, GenerateSprite(ObjInfoScript.textureA)));
        options.Add(new TMP_Dropdown.OptionData(DropDownLabelB, GenerateSprite(ObjInfoScript.textureB)));
        options.Add(new TMP_Dropdown.OptionData(DropDownLabelC, GenerateSprite(ObjInfoScript.textureC)));
        dropdown.AddOptions(options);

        //optionAlabel.text = ObjInfoScript.textureAName + " ($" + ObjInfoScript.costA.ToString() + ")";
        //optionBlabel.text = ObjInfoScript.textureBName + " ($" + ObjInfoScript.costB.ToString() + ")";
        //optionClabel.text = ObjInfoScript.textureCName + " ($" + ObjInfoScript.costC.ToString() + ")";

        //currentImage.sprite = GenerateSprite(ObjInfoScript.currentTexture);
        //optionAImage.sprite = GenerateSprite(ObjInfoScript.textureA);
        //optionBImage.sprite = GenerateSprite(ObjInfoScript.textureB);
        //optionCImage.sprite = GenerateSprite(ObjInfoScript.textureC);

        //optionAImage = ObjInfoScript.imageA;
        //optionBImage = ObjInfoScript.imageB;
        //optionCImage = ObjInfoScript.imageC;
    }
    void DropdownListener(int index)
    {
        if (index == 0) { ObjInfoScript.changeTexture(ObjectInformation.option.A); }
        if (index == 1) { ObjInfoScript.changeTexture(ObjectInformation.option.B); }
        if (index == 2) { ObjInfoScript.changeTexture(ObjectInformation.option.C); }
    }
    Sprite GenerateSprite(Texture REFTexture)
    {
        return Sprite.Create((Texture2D)REFTexture, new Rect(0, 0, REFTexture.width, REFTexture.height), new Vector2(0.5f, 0.5f));
    }
    public void AlexActivateMePleaseThankYouOBJINFO()
    {

    }

    //void Update()
    //{
    //    
    //}
}
