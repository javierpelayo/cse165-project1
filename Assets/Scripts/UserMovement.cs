using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class UserMovement : MonoBehaviour
{

    // Start is called before the first frame update

    public float speed = 1.0f;
    public Transform cameraTransform;


    // Update is called once per frame
    void Update()
    {
         // Get the input from the left joystick
        Vector2 joystickInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Calculate the forward movement
        //Vector3 forwardMovement = cameraTransform.forward * joystickInput.y;
        Vector3 forwardMovement = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up) * joystickInput.y;
        // Calculate the rightward movement
        //Vector3 rightwardMovement = cameraTransform.right * joystickInput.x;
        Vector3 rightwardMovement = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up) * joystickInput.x;

        // Combine the movement vectors and normalize to avoid faster diagonal movement
        Vector3 movement = (forwardMovement + rightwardMovement).normalized * speed * Time.deltaTime;

        // Apply the movement
        transform.Translate(movement, Space.World);
    }


}
