using UnityEngine;
using System.Collections;

public class ObjectInformation : MonoBehaviour
{

    /*
    void Update()
    {
        
    }*/

    public enum option { A, B, C };
    [Header("Current Option:")]
    public option ActiveTexture = option.A;

    [Header("ASSIGN TEXTURE INFORMATION")]
    public Texture textureA;
    public int costPerUnitA;

    public Texture textureB;
    public int costPerUnitB;

    public Texture textureC;
    public int costPerUnitC;
    
    //[Header("ASSIGN TEXTURE INFORMATION")]
    //public Material materialA;
    //public int costPerUnitA;
    //
    //public Material materialB;
    //public int costPerUnitB;
    //
    //public Material materialC;
    //public int costPerUnitC;

    [Header("ASSIGN OBJECT INFORMATION")]
    public string CustomName = "";
    public bool moveableObject = false;
    public int UnitQuantity = 0;



    [Space(60)]
    [Header("------- IGNORE BELOW -------")]

    [Header("Debug Testing:")]
    public string DebugInput = "";
    public bool executeInput = false;

    [Header("Force Cost (Ignore if you don't know what you're doing)")]
    public int costA;
    public int costB;
    public int costC;

    [Header("Current Information (for debug purposes)")]
    public int textureCount = 0;
    public int CurrentCost;

    public CostUnits unitOfMeasurement = CostUnits.None;

    private MeshRenderer meshRenderer; //NOT USING RN
    public Renderer rend;
    public CostData CostDataScript;
    void Start()
    {
        //Sanity Checks
        if (textureA == null && textureB == null && textureC == null)
        {
            Debug.LogWarning("It seems you forgot to put a textures for \"" + this.gameObject.name + "\" !" +
                "\n will return to avoid issues.");
            return;
        }
        if (UnitQuantity <= 0) { Debug.LogWarning("Seems like no unit quantity was assigned to \"" + gameObject.name + "\"!"); }

        //Superceed dumb logic (idiot proofing) --> rearranging textures so A always has a texture then B then C.
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

        //Linking
        if (meshRenderer == null) { meshRenderer = GetComponent<MeshRenderer>(); } //NOT USING RN
        if (meshRenderer == null) { Debug.LogWarning("Failed to get MeshRendered component"); }
        if (rend == null) { rend = GetComponent<Renderer>(); }
        if (rend == null) { Debug.LogWarning("Failed to get MeshRendered component"); }
        if (CostDataScript == null)
        {
            GameObject obj = GameObject.FindWithTag("CostTracker");
            if (obj != null) { CostDataScript = obj.GetComponent<CostData>(); }
            if (CostDataScript != null)
            {
                Debug.LogWarning("ObjectInformation.cs script on " + gameObject.name +
                " was not able to find the CostData script.");
            }

            //some calculations and stuff
            if (textureA != null) { textureCount++; }
            if (textureB != null) { textureCount++; }
            if (textureC != null) { textureCount++; }
            if (CustomName == "") { CustomName = this.gameObject.name; }
            CurrentCost = calculateCost();
            if (CurrentCost == 0) { Debug.LogWarning("Warning, " + CustomName + " calculated cost = 0!"); }

            //Push to Cost Data database gameObject
        }

        StartCoroutine(DebugTestingRoutine());
    }

    public int calculateCost()
    {
        if (ActiveTexture == option.A)
        {
            return UnitQuantity * costA;
        }
        if (ActiveTexture == option.B)
        {
            return UnitQuantity * costB;
        }
        if (ActiveTexture == option.C)
        {
            return UnitQuantity * costC;
        }
        return 0;
    }

    //NEED A WAY TO TRIGGER THIS

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
        this.ActiveTexture = option;
        if (ActiveTexture == option.A) { rend.material.mainTexture = textureA; }
        if (ActiveTexture == option.B) { rend.material.mainTexture = textureB; }
        if (ActiveTexture == option.C) { rend.material.mainTexture = textureC; }
        CurrentCost = calculateCost();
    }

    IEnumerator DebugTestingRoutine()
    {
        while (true)
        {
            if (executeInput)
            {
                executeInput = false;
                //DEBUG FUNCTION START

                if (DebugInput == "A") { changeTexture(option.A); }
                if (DebugInput == "B") { changeTexture(option.B); }
                if (DebugInput == "C") { changeTexture(option.C); }

                //DEBUG FUNCTION END
            }

            yield return new WaitForSeconds(1f); // wait 1 second
        }
    }
}