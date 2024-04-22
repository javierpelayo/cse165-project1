using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderRay : MonoBehaviour
{
    public LineRenderer lr;
    public float rayLength = 100.0f;

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

        bool rayHits = Physics.Raycast(Vector3.zero, new Vector3(0,0,1), out hit, rayLength);

        if(rayHits)
        {
            endPosition = new Vector3(0.0f, 0.0f, hit.distance);
            Debug.Log("Hit distance: " + hit.distance);
        }

        // Debug

        lr.SetPosition(0, Vector3.zero); // start
        lr.SetPosition(1, endPosition); // end

    }
}
