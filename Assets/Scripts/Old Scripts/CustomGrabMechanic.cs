using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrabMechanic : MonoBehaviour
{
    private GameObject heldObject = null;
    private List<GameObject> potentialGrabbableObjects = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            potentialGrabbableObjects.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            potentialGrabbableObjects.Remove(other.gameObject);
        }
    }

    void Update()
    {
        // Check for grab input
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            if (potentialGrabbableObjects.Count > 0)
            {
                GameObject closestObject = FindClosestGrabbableObject();
                if (closestObject != null)
                {
                    GrabObject(closestObject);
                }
            }
        }

        // Check for release input
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger) && heldObject != null)
        {
            ReleaseObject();
        }
    }

    GameObject FindClosestGrabbableObject()
    {
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject obj in potentialGrabbableObjects)
        {
            Vector3 directionToTarget = obj.transform.position - position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistance)
            {
                closestDistance = dSqrToTarget;
                closestObject = obj;
            }
        }
        return closestObject;
    }

    void GrabObject(GameObject obj)
    {
        // Parent it to the hand anchor
        heldObject = obj;
        heldObject.transform.SetParent(transform);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;

        // Disable physics while holding the object
        var rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    void ReleaseObject()
    {
        // Re-enable physics on release
        var rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch); // Apply velocity based on controller movement
        }
        
        // Unparent the object
        heldObject.transform.SetParent(null);
        heldObject = null;
    }
}
