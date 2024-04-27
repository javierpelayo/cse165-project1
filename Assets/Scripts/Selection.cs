using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private GameObject selectedObject;
    public Material highlightMaterial;
    private Material originalMaterial;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Grabbable"))
                {
                    if (selectedObject != null)
                    {
                        // Deselect the previous object
                        DeselectObject();
                    }
                    
                    // Select the new object
                    selectedObject = hitCollider.gameObject;
                    originalMaterial = selectedObject.GetComponent<Renderer>().material;
                    selectedObject.GetComponent<Renderer>().material = highlightMaterial;
                    break;
                }
            }
        }
    }

    private void DeselectObject()
    {
        if (selectedObject != null)
        {
            // Restore the original material
            selectedObject.GetComponent<Renderer>().material = originalMaterial;
            selectedObject = null;
        }
    }
}
