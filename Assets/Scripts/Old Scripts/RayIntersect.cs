using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayIntersect : MonoBehaviour
{
    void FixedUpdate() {

        // Get the main cameras forward direction
        Vector3 forwardDirection = transform.forward;
        forwardDirection.Normalize();

        // do Physics.Raycast
        // store information about collider in hit variable
        RaycastHit hit;
        bool rayHits = Physics.Raycast(transform.position, forwardDirection, out hit, Mathf.Infinity);

        if(rayHits)
        {
            Debug.DrawRay(transform.position, forwardDirection * hit.distance, Color.yellow);
            Debug.Log("Hit! distance: " + hit.distance + " Position: " + hit.transform.position);
        } else
        {
            Debug.Log("Did not hit!");
        }

    }
}
