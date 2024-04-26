using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderRay : MonoBehaviour
{
    public LineRenderer lr;
    public float rayLength = 10.0f;

    public GameObject itemPrefab;  // Drag your prefab here in the inspector
    public LayerMask interactableLayers; 

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = new Ray(new Vector3(0,0,0), new Vector3(0,0,1));
        RaycastHit hit;
        Vector3 endPosition = new Vector3(0,0,rayLength);

        bool rayHits = Physics.Raycast(transform.position, transform.forward, out hit, rayLength);

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (rayHits) {
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

        if(rayHits)
        {
            endPosition = new Vector3(0.0f, 0.0f, hit.distance);
        }

        lr.SetPosition(0, Vector3.zero); // start
        lr.SetPosition(1, endPosition); // end
    }
}
