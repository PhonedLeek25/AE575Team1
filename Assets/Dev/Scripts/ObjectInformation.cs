using System.Collections;
using UnityEngine;
public class ObjectInformation : MonoBehaviour
{
    public enum option { A, B, C };
    [Header("Current Option:")]
    public option ActiveTextureOption = option.A;
    public Texture currentTexture;
    public int currentCost;
    public string currentTextureName;
    //public Image currentImage;

    [Header("ASSIGN OBJECT INFORMATION:")]
    public string CustomName = "";
    public bool moveableObject = false;
    public GameObject objectUI;

    [Header("ASSIGN TEXTURE INFORMATION")]
    [Space(10)]
    public string textureAName = "TextureA";
    public Texture textureA;
    //public int costPerUnitA;
    public int costA;
    [Space(10)]
    public string textureBName = "TextureB";
    public Texture textureB;
    //public int costPerUnitB;
    public int costB;
    [Space(10)]
    public string textureCName = "TextureC";
    public Texture textureC;
    //public int costPerUnitC;
    public int costC;

    //[Header("Derived Image from Textures")]
    //public Image imageA;
    //public Image imageB;
    //public Image imageC;

    //[Header("ASSIGN MATERIAL INFORMATION")]
    //public Material materialA;
    //public int costPerUnitA;
    //
    //public Material materialB;
    //public int costPerUnitB;
    //
    //public Material materialC;
    //public int costPerUnitC;

    [Space(60)]
    [Header("------- IGNORE BELOW -------")]

    [Header("Debug Testing:")]
    public string DebugInput = "";
    public bool DebugMode = false;

    [Header("Current Information (for debug purposes)")]
    public int textureCount = 0;

    private MeshRenderer meshRenderer; //NOT USING RN
    public Renderer rend;
    public CostData CostDataScript;
    void Awake()
    {
        //Must Be Set at Compile Time
        gameObject.layer = LayerMask.NameToLayer("InteractableRaycast");
        var count = GetComponents<ObjectInformation>().Length;
        if (count > 1) { Debug.LogError($"{gameObject.name} has {count} ObjectInformation components!", this); }
        /*imageA.sprite = Sprite.Create((Texture2D)textureA, new Rect(0, 0, textureA.width, textureA.height), new Vector2(0.5f, 0.5f));
        imageB.sprite = Sprite.Create((Texture2D)textureB, new Rect(0, 0, textureB.width, textureB.height), new Vector2(0.5f, 0.5f));
        imageC.sprite = Sprite.Create((Texture2D)textureC, new Rect(0, 0, textureC.width, textureC.height), new Vector2(0.5f, 0.5f));*/

        //Sanity Checks
        if (textureA == null && textureB == null && textureC == null)
        {
            Debug.LogWarning("It seems you forgot to put a textures for \"" + this.gameObject.name + "\" !" +
                "\n will return to avoid issues.");
            Debug.Log($"[{gameObject.name}] A={textureA}, B={textureB}, C={textureC}, instanceID={GetInstanceID()}", this);
            return;
        }
        //if (UnitQuantity <= 0) { Debug.LogWarning("Seems like no unit quantity was assigned to \"" + gameObject.name + "\"!"); }

        //Superceed dumb logic (idiot proofing)
        if (textureA == null && textureB != null)
        {
            textureA = textureB;
            textureB = null;
        }
        else if (textureA == null && textureC != null)
        {
            textureA = textureC;
            textureC = null;
        }
        if (textureB == null && textureC != null)
        {
            textureB = textureC;
            textureC = null;
        }

        //Calcs before linking
        if (textureA != null) { textureCount++; }
        if (textureB != null) { textureCount++; }
        if (textureC != null) { textureCount++; }
        if (CustomName == "") { CustomName = this.gameObject.name; }
        currentCost = calculateCost();
        if (currentCost == 0) { Debug.LogWarning("Warning, " + CustomName + " calculated cost = 0!"); }

        //Linking
        if (meshRenderer == null) { meshRenderer = GetComponent<MeshRenderer>(); } //NOT USING RN
        if (meshRenderer == null) { Debug.LogWarning("Failed to get MeshRendered component"); }
        if (rend == null) { rend = GetComponent<Renderer>(); }
        if (rend == null) { Debug.LogWarning("Failed to get MeshRendered component"); }

        //Final Calculations and Setup
        changeTexture(ActiveTextureOption);

    }
    void Start()
    {
        //Link to CostData script
        if (CostDataScript == null)
        {
            GameObject obj = GameObject.FindWithTag("CostTracker");
            if (obj != null)
            {
                CostDataScript = obj.GetComponent<CostData>();
                if (CostDataScript == null) { Debug.LogWarning("Found Cost Tracker, but unable to find in components CostData script"); }
            }
            else { Debug.LogWarning("Failed to find gameObject with tag \"CostTracker\""); }
        }
        if (!CostDataScript.ObjectsInScene.Contains(this)) { CostDataScript.ObjectsInScene.Add(this); }

        //Link to ObjUIScript
        ObjectUIHandler ObjUIScript = GetComponentInChildren<ObjectUIHandler>();
        if (ObjUIScript != null) { objectUI = ObjUIScript.gameObject; }
        else { Debug.LogWarning("No ObjUIScript found in children of " + CustomName); }

        //Debug Testing
        StartCoroutine(DebugTestingRoutine());
    }

    public int calculateCost()
    {
        if (ActiveTextureOption == option.A)
        {
            //return UnitQuantity * costA;
            return costA;
        }
        if (ActiveTextureOption == option.B)
        {
            //return UnitQuantity * costB;
            return costB;
        }
        if (ActiveTextureOption == option.C)
        {
            //return UnitQuantity * costC;
            return costC;
        }
        return 0;
    }

    //public void changeMaterial(option option)
    //{
    //    this.ActiveTexture = option;
    //    if (ActiveTexture == option.A) { meshRenderer.material = materialA; }
    //    if (ActiveTexture == option.B) { meshRenderer.material = materialB; }
    //    if (ActiveTexture == option.C) { meshRenderer.material = materialC; }
    //    CurrentCost = calculateCost();
    //}
    public void changeTexture(option option)
    {
        this.ActiveTextureOption = option;
        //Update "Current Information"
        if (ActiveTextureOption == option.A) { currentTexture = textureA; currentCost = costA; currentTextureName = textureAName; }
        if (ActiveTextureOption == option.B) { currentTexture = textureB; currentCost = costB; currentTextureName = textureBName; }
        if (ActiveTextureOption == option.C) { currentTexture = textureC; currentCost = costC; currentTextureName = textureCName; }
        //Apply to render

        //rend.material.mainTexture = currentTexture;
        Material[] mats = rend.materials;
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].mainTexture = currentTexture;
        }
        rend.materials = mats;

    }

    public void ActivateUI()
    {
        if (objectUI != null) { objectUI.SetActive(true); }
        else { Debug.LogWarning("Recieved Clicked() order on " + CustomName + " but no objectUI Object set!"); }
    }
    public void DeactivateUI()
    {
        if (objectUI != null) { objectUI.SetActive(false); }
    }

    IEnumerator DebugTestingRoutine()
    {
        while (true)
        {
            if (DebugMode)
            {
                DebugMode = false;
                //DEBUG FUNCTION START

                if (DebugInput == "A") { changeTexture(option.A); }
                if (DebugInput == "B") { changeTexture(option.B); }
                if (DebugInput == "C") { changeTexture(option.C); }
                if (DebugInput == "E") { CostDataScript.ShowCurrentList(); }

                //DEBUG FUNCTION END
            }

            yield return new WaitForSeconds(1f); // wait 1 second
        }
    }

    private void OnDisable()
    {
        if (CostDataScript != null) { CostDataScript.ObjectsInScene.Remove(this); }
    }
}