using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSelection : MonoBehaviour
{
    public Transform rayOrigin;
    public float rayLength = 10.0f;
    private GameObject selectedObject;
    public Material highlightMaterial;
    private Material originalMaterial;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (selectedObject != null)
            {
                DeselectObject();
            }
            
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, rayLength))
            {
                if (hit.collider.CompareTag("Grabbable"))
                {
                    selectedObject = hit.collider.gameObject;
                    originalMaterial = selectedObject.GetComponent<Renderer>().material;
                    selectedObject.GetComponent<Renderer>().material = highlightMaterial;
                }
            }
        }
    }

    private void DeselectObject()
    {
        if (selectedObject != null)
        {
            selectedObject.GetComponent<Renderer>().material = originalMaterial;
            selectedObject = null;
        }
    }

    void OnDrawGizmos()
    {
        // To visualize the ray in the scene view
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + rayOrigin.forward * rayLength);
    }
}
