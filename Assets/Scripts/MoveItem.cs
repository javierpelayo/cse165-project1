using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SelectItem;

public class MoveItem : MonoBehaviour
{

    public GameObject controller;
    public float distanceToItem = 3.0f;
    public static Collider collider;
    public static Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("RightLineRay");
    }

    // Update is called once per frame
    void Update()
    {
        bool rightGripDown = OVRInput.Get(OVRInput.Button.SecondaryHandTrigger);

        if (RenderRay.rayHitsCollider && rightGripDown && SelectItem.selectedItem != null)
        {
            Collider c = SelectItem.selectedItem.GetComponent<Collider>();
            Rigidbody rb = SelectItem.selectedItem.GetComponent<Rigidbody>();
            rigidbody = rb;
            collider = c;
            collider.enabled = false;
            rigidbody.useGravity = false;
            rigidbody.mass = 0.0f;
            rigidbody.isKinematic = true;
            //distanceToItem = RenderRay.isectInfo.distance;
        }

        if(!rightGripDown && collider != null)
        {
            collider.enabled = true;
            rigidbody.useGravity = true;
            rigidbody.mass = 1.0f;
            rigidbody.isKinematic = false;
            SelectItem.selectedItem = null;
        }

        if (SelectItem.selectedItem != null)
        {
            SelectItem.selectedItem.transform.position = new Vector3(controller.transform.position.x + controller.transform.forward.x * distanceToItem,
                                                                controller.transform.position.y + controller.transform.forward.y * distanceToItem,
                                                                controller.transform.position.z + controller.transform.forward.z * distanceToItem);
        }
               
    }
}
