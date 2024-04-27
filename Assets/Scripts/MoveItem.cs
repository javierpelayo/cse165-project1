using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SelectItem;

public class MoveItem : MonoBehaviour
{

    public GameObject controller;
    public float distanceToItem = 3.0f;
    public Collider collider;
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
            collider = c;
            collider.enabled = false;
            //distanceToItem = RenderRay.isectInfo.distance;
        }

        if(!rightGripDown)
        {
            collider.enabled = true;
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
