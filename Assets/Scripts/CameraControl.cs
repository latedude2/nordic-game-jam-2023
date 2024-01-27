using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{    public float sensitivity = 2.0f; // Camera sensitivity
    public float minimumY = -80.0f; // Minimum vertical angle
    public float maximumY = 80.0f; // Maximum vertical angle
    private float cameraTruckAdjustment = 0.002f; 

    private float rotationY = 0.0f;
    void Start()
    {
        //lock mouse to center of screen
        Cursor.lockState = CursorLockMode.Locked;
        CarEnterHandle.onEnter.AddListener(ResetCameraRotation);
    }

    void ResetCameraRotation()
    {
        Debug.Log("Reset camera rotation");
        transform.localEulerAngles = new Vector3(0, 0, 0);
        rotationY = 0;
    }

    void Update()
    {
        Debug.Log(transform.localEulerAngles);
        // Rotate the camera based on the mouse movement
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;

        rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        //limit camera rotation to the left
        if (rotationX < 280 && rotationX > 225)
        {
            rotationX = 280;
        }
        else if (rotationX > 180 && rotationX < 225)
        {
            rotationX = 180;
        }

        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0.0f);
    
        if(transform.localEulerAngles.y < 180)
        {
            //move camera to the right
            transform.localPosition = new Vector3(transform.localEulerAngles.y * cameraTruckAdjustment, 0, 0);
        }
        else if(transform.localEulerAngles.y > 270)
        {
            //move camera to the left
            transform.localPosition = new Vector3(0, 0, 0);
        }
        else
        {
            //move camera to the left
            transform.localPosition = new Vector3(180 * cameraTruckAdjustment, 0, 0);
        }

    }

    
}
