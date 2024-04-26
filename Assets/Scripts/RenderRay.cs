using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderRay : MonoBehaviour
{
    public LineRenderer lr;
    public float rayLength = 10.0f;

    public GameObject itemPrefab1;  // Drag your prefab here in the inspector
    public GameObject itemPrefab2;  // Drag your prefab here in the inspector

    public LayerMask interactableLayers;
    public Camera mainCamera = Camera.main;
    public GameObject mainCanvas;
    public GameObject chosenObject;

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

        bool rayHitsCollider = Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity);
        bool rightTriggerDown = OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger);

        //if (rayHitsCollider && hit.collider.gameObject == mainCanvas) {
        if (rayHitsCollider) {
            Debug.Log("Hit! Hit Point: " + hit.point);
            endPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);

            if (hit.collider.gameObject == GameObject.Find("ButtonTool1") && rightTriggerDown) {
                Debug.Log("Tool1!");
                chosenObject = itemPrefab1;
            } else if (hit.collider.gameObject == GameObject.Find("ButtonTool2") && rightTriggerDown) {
                Debug.Log("Tool2!");
                chosenObject = itemPrefab2;
            } else if (rightTriggerDown && chosenObject != null) {
                // If the ray hits, spawn the item at the hit point
                Vector3 spawnPoint = new Vector3(hit.point.x, hit.point.y + 1.0f, hit.point.z);
                GameObject inst = Instantiate(chosenObject, spawnPoint, Quaternion.identity);
                Rigidbody rb = inst.AddComponent<Rigidbody>();
                BoxCollider bc = inst.AddComponent<BoxCollider>();

                bc.isTrigger = false;
                bc.size = new Vector3(inst.GetComponent<Renderer>().bounds.size.x, inst.GetComponent<Renderer>().bounds.size.y, inst.GetComponent<Renderer>().bounds.size.z);
                rb.useGravity = true;
                rb.mass = 1.0f;
            } else {
                // If the ray does not hit, spawn the item at the maximum ray length
                //Instantiate(itemPrefab, transform.position + transform.forward * rayLength, Quaternion.identity);
            }
        } else {
            Debug.Log("Not Hit!");
        }

        Debug.Log("Controller: transform position: " + transform.position + ", transform forward:" + transform.forward);
        Debug.Log("Main Camera: transform position: " + mainCamera.transform.position + ", transform forward:" + mainCamera.transform.forward);

        lr.SetPosition(0, transform.position); // start
        lr.SetPosition(1, endPosition); // end
    }
}
