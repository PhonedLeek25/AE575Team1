using UnityEngine;

public class ObjectInformation : MonoBehaviour
{

    /*
    void Update()
    {
        
    }*/

    public enum option { A, B, C };
    [Header("Current Option:")]
    public option ActiveTexture = option.A;

    [Header("ASSIGN MATERIAL/TEXTURE INFORMATION")]
    public Material materialA;
    public int costPerUnitA;

    public Material materialB;
    public int costPerUnitB;

    public Material materialC;
    public int costPerUnitC;

    [Header("ASSIGN OBJECT INFORMATION")]
    public string CustomName = "";
    public bool moveableObject = false;
    public int UnitQuantity = 0;



    [Space(60)]
    [Header("------- IGNORE BELOW -------")]

    [Header("Force Cost (Ignore if you don't know what you're doing)")]
    public int costA;
    public int costB;
    public int costC;

    [Header("Current Information (for debug purposes)")]
    public int textureCount = 0;
    public int CurrentCost;

    public CostUnits unitOfMeasurement = CostUnits.None;

    private MeshRenderer meshRenderer;
    public CostData CostDataScript;
    void Start()
    {
        //Sanity Checks
        if (materialA == null && materialB == null && materialC == null)
        {
            Debug.LogWarning("It seems you forgot to put a textures for \"" + this.gameObject.name + "\" !" +
                "\n will return to avoid issues.");
            return;
        }
        if (UnitQuantity <= 0) { Debug.LogWarning("Seems like no unit quantity was assigned to \"" + gameObject.name + "\"!"); }

        //Superceed dumb logic (idiot proofing) --> rearranging textures so A always has a texture then B then C.
        if (materialA == null && materialB != null)
        {
            materialA = materialB;
            materialB = null;
        }
        else if (materialA == null && materialC != null)
        {
            materialA = materialC;
            materialC = null;
        }
        if (materialB == null && materialC != null)
        {
            materialB = materialC;
            materialC = null;
        }

        //Linking
        meshRenderer = GetComponent<MeshRenderer>();
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
            if (materialA != null) { textureCount++; }
            if (materialB != null) { textureCount++; }
            if (materialC != null) { textureCount++; }
            if (CustomName == "") { CustomName = this.gameObject.name; }
            CurrentCost = calculateCost();
            if (CurrentCost == 0) { Debug.LogWarning("Warning, " + CustomName + " calculated cost = 0!"); }

            //Push to Cost Data database gameObject
        }

        int calculateCost()
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
        void changeTexture(option option)
        {
            this.ActiveTexture = option;
            if (ActiveTexture == option.A) { meshRenderer.material = materialA; }
            if (ActiveTexture == option.B) { meshRenderer.material = materialB; }
            if (ActiveTexture == option.C) { meshRenderer.material = materialC; }
            CurrentCost = calculateCost();
        }
    }
}