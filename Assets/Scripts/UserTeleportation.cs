using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserTeleportation : MonoBehaviour
{
    public Transform cameraRig;         // Reference to the VR Camera Rig
    public Transform cameraEye;         // Reference to the VR Camera (typically the center eye)
    public LineRenderer teleportLine;   // Line renderer to show the teleportation path
    public LayerMask teleportMask;      // LayerMask to specify which layers are teleportable
    public float maxTeleportDistance = 10f;  // Maximum teleport distance

    private bool isTeleporting = false;
    private Vector3 hitPoint;  // Destination point of teleport

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))  // Start teleport mode (change button as needed)
        {
            isTeleporting = true;
            teleportLine.enabled = true;
        }

        if (isTeleporting)
        {
            Ray ray = new Ray(cameraEye.position, cameraEye.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, maxTeleportDistance, teleportMask))
            {
                hitPoint = hit.point;
                teleportLine.SetPositions(new Vector3[] { cameraEye.position, hitPoint });
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))  // Perform teleport (change button as needed)
        {
            isTeleporting = false;
            teleportLine.enabled = false;
            cameraRig.position = new Vector3(hitPoint.x, cameraRig.position.y, hitPoint.z);  // Teleport the camera rig but maintain the original height
        }
    }
}
