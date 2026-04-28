using UnityEngine;

public class ObjectSwitch : MonoBehaviour
{
    public enum option { A, B, C};
    public option activeOption = option.A;
    public GameObject objectA;
    public GameObject objectB;
    public GameObject objectC;
    // Start is called before the first frame update
    void Start()
    {
        if (objectA == null && objectB == null && objectC == null) { Debug.LogWarning("It seems you forgot to assign any objects."); return; }
        if (objectA == null && objectB != null) { objectA = objectB; objectB = null; }
        if (objectB == null && objectC != null) { objectB = objectC; objectC = null; }
        if (objectC == null && objectC != null) { objectB = objectC; objectC = null; }
    }

}
