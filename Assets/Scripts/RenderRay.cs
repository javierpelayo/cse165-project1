using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderRay : MonoBehaviour
{
    public LineRenderer lr;
    public float rayLength = 100.0f;
    public Transform controllerTransform;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = new Ray(controllerTransform.position, Camera.main.transform.forward);
        RaycastHit hit;
        Vector3 endPosition = controllerTransform.position + Camera.main.transform.forward * rayLength;

        bool rayHits = Physics.Raycast(ray, out hit, rayLength);

        if(rayHits)
        {
            endPosition = hit.point;
            Debug.Log("Hit: " + endPosition);
        }

        lr.SetPosition(0, Vector3.zero); // start
        lr.SetPosition(1, endPosition); // end

    }
}
