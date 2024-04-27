using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static RenderRay;

public class SelectItem : MonoBehaviour
{

    public Color highlightColor = Color.yellow;
    public Color hdrEmissionColor = new Color(100.0f, 100.0f, 100.0f, 1.0f);
    public Color originalColor = Color.white;
    public MenuItems menuItems;

    public static GameObject selectedItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool rightGripDown = OVRInput.Get(OVRInput.Button.SecondaryHandTrigger);

        if (RenderRay.rayHitsCollider && rightGripDown) {
            GameObject objectCollided = RenderRay.isectInfo.collider.gameObject;

            //bool instanceOfPrefab1 = PrefabUtility.GetPrefabInstanceHandle(objectCollided) == PrefabUtility.GetPrefabInstanceHandle(menuItems.itemPrefab1);
            //bool instanceOfPrefab2 = PrefabUtility.GetPrefabInstanceHandle(objectCollided) == PrefabUtility.GetPrefabInstanceHandle(menuItems.itemPrefab2);

            if (objectCollided.Equals(GameObject.Find("Equipment_36(Clone)")) || objectCollided.Equals(GameObject.Find("Bin_1(Clone)")))
            {
                Debug.Log("Item Selected!");
                selectedItem = RenderRay.isectInfo.collider.gameObject;
                HighlightItem();
            }
        }        
    }

    void HighlightItem()
    {
        Renderer r = selectedItem.GetComponent<Renderer>();
        if (r != null)
        {
            Debug.Log("Got Renderer!!!");
            Material m = r.material;
            m.EnableKeyword("_EMISSION");
            m.SetColor("_EmissiveColor", hdrEmissionColor);
            m.SetFloat("_EmissiveIntensity", 1.0f);
        }
    }
}
