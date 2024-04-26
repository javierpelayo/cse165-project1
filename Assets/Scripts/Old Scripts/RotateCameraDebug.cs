using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraDebug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform != RuntimePlatform.Android) {
            // using up and down arrow keys to rotate the camera up and down
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Rotate(-1, 0, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Rotate(1, 0, 0);
            }
        
            // using left and right arrow keys to rotate the camera left and right
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(0, -1, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(0, 1, 0);
            }
        }
        
    }
}