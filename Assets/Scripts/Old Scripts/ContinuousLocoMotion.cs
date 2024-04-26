using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousLocoMotion : MonoBehaviour
{
    public float speed = 2.0f; // Speed of the player movement
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android) {
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            Vector3 move = new Vector3(joystickInput.x, 0, joystickInput.y);

            // want to rotate move to orientate with camera direction
            Vector3 playerForward = transform.forward;

            float pfAngle = getAngle(playerForward);
            float mAngle = getAngle(move);

            float rotAngle = (pfAngle - mAngle) * Mathf.Rad2Deg;
            Vector3 rotAxis = Vector3.up;

            Quaternion rotQuat = Quaternion.AngleAxis(rotAngle, rotAxis);
            move = rotQuat * move;

            move = transform.TransformDirection(move); // Ensure movement is relative to player's facing direction
        
            // NOTE: Time.deltaTime is time in seconds it took to complete previous frame
            //       , it is important for smooth movement
            characterController.Move(move * speed * Time.deltaTime);
        } else {
            // DEBUG
            // wasd for moving

            bool forward = Input.GetKey(KeyCode.W);
            bool backward = Input.GetKey(KeyCode.S);
            bool left = Input.GetKey(KeyCode.A);
            bool right = Input.GetKey(KeyCode.D);
            Vector3 move = new Vector3(0,0,0);

            if (forward) {
                move = new Vector3(0,0,1);
            } else if (backward) {
                move = new Vector3(0,0,-1);
            } else if (left) {
                move = new Vector3(1,0,0);
            } else if (right) {
                move = new Vector3(-1,0,0);
            }

            // want to rotate move to orientate with camera direction
            Vector3 playerForward = transform.forward;
            Debug.Log(playerForward);

            float pfAngle = getAngle(playerForward);
            float mAngle = getAngle(move);

            float rotAngle = (pfAngle - mAngle) * Mathf.Rad2Deg;
            Vector3 rotAxis = Vector3.up;

            Quaternion rotQuat = Quaternion.AngleAxis(rotAngle, rotAxis);
            move = rotQuat * move;

            move = transform.TransformDirection(move); // Ensure movement is relative to player's facing direction
        
            // NOTE: Time.deltaTime is time in seconds it took to complete previous frame
            //       , it is important for smooth movement
            characterController.Move(move * speed * Time.deltaTime);
        }
    }

    float getAngle(Vector3 vec) {
        float thetaRad1 = Mathf.Asin(vec.z / Mathf.Sqrt((vec.x * vec.x) + (vec.z * vec.z)) );
        float totalThetaRad = 0.0f;

        // We want to get theta in terms of counterclockwise rotation

        if (vec.x > 0 && vec.y > 0) {
            // In quadrant 1
            totalThetaRad = thetaRad1;
        } else if (vec.x < 0 && vec.y > 0) {
            // In quadrant 2
            totalThetaRad = 90.0f + thetaRad1;
        } else if (vec.x < 0 && vec.y < 0) {
            // In quadrant 3
            totalThetaRad = 180.0f + thetaRad1;
        } else if (vec.x > 0 && vec.y < 0) {
            // in quadrant 4
            totalThetaRad = 0.0f - thetaRad1;
        }

        return totalThetaRad;
    }
}


