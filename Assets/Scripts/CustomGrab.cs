using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrab : MonoBehaviour
{
    public Transform grabPoint; // The point from where objects will be grabbed
    private GameObject heldObject = null;
    private Rigidbody heldObjectRigidbody = null;

    void Update()
    {
        // Check for grab input
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            AttemptGrab();
        }

        // Check for release input
        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            ReleaseObject();
        }
    }

    private void AttemptGrab()
    {
        Collider[] grabbableColliders = Physics.OverlapSphere(grabPoint.position, 0.1f); // Small radius for grabbing
        foreach (var collider in grabbableColliders)
        {
            if (collider.CompareTag("Grabbable")) // Ensure the object has the "Grabbable" tag
            {
                heldObject = collider.gameObject;
                heldObjectRigidbody = heldObject.GetComponent<Rigidbody>();

                // Parent it to the controller's grab point, and disable physics
                heldObject.transform.SetParent(grabPoint, true);
                heldObject.transform.localPosition = Vector3.zero;
                heldObject.transform.localRotation = Quaternion.identity;
                if (heldObjectRigidbody != null)
                {
                    heldObjectRigidbody.isKinematic = true;
                }
                break; // Only grab the first object
            }
        }
    }

    private void ReleaseObject()
    {
        if (heldObject != null)
        {
            // Unparent the object and re-enable physics
            heldObject.transform.SetParent(null, true);
            if (heldObjectRigidbody != null)
            {
                heldObjectRigidbody.isKinematic = false;
                heldObjectRigidbody.velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch); // Apply controller's velocity to the object
                heldObjectRigidbody.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);
            }
            // Clear references
            heldObject = null;
            heldObjectRigidbody = null;
        }
    }

    private void OnDrawGizmos()
    {
        // Just to visualize the grab point in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(grabPoint.position, 0.1f);
    }
}
