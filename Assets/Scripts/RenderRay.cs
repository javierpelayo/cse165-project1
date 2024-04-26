using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderRay : MonoBehaviour
{
    public LineRenderer lr;
    public float rayLength = 10.0f;

    public GameObject itemPrefab;  // Drag your prefab here in the inspector
    public LayerMask interactableLayers;
    public Camera mainCamera = Camera.main;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Vector3 endPosition = new Vector3(transform.position.x + transform.forward.x * rayLength, transform.position.y + transform.forward.y * rayLength, transform.position.z + transform.forward.z * rayLength);

        bool rayHits = Physics.Raycast(transform.position, transform.forward, out hit, rayLength);

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (rayHits)
            {
                // If the ray hits, spawn the item at the hit point
                Instantiate(itemPrefab, hit.point, Quaternion.identity);
            }
            else
            {
                // If the ray does not hit, spawn the item at the maximum ray length
                Instantiate(itemPrefab, transform.position + transform.forward * rayLength, Quaternion.identity);
            }
        }

        Debug.Log("Controller: transform position: " + transform.position + ", transform forward:" + transform.forward);
        Debug.Log("Main Camera: transform position: " + mainCamera.transform.position + ", transform forward:" + mainCamera.transform.forward);

        if(rayHits) {
            //endPosition = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, transform.position.z + hit.distance);
            Debug.Log("Hit!");
        } else {
            Debug.Log("Not Hit!");
        }

        lr.SetPosition(0, transform.position); // start
        lr.SetPosition(1, endPosition); // end
    }
}
