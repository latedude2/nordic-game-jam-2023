using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraControl : MonoBehaviour
{    
    public float sensitivity = 2.0f; // Camera sensitivity
    public float minimumY = -80.0f; // Minimum vertical angle
    public float maximumY = 80.0f; // Maximum vertical angle

    public float minimumX = 180; // Maximum horizontal angle
    public float maximumX = 210; // Minimum horizontal angle

    public float minShoulderLook = 180;
    public float maxShoulderLook = 270;
    private float cameraTruckAdjustment = 0.002f; 
    public float positionOffset = 0;

    [MaxValue(1)]
    public float positionOffsetLeftFraction = 1f;
    
    [MaxValue(1)]
    public float positionOffsetRightFraction = 1f;

    public float rotationX = 0;

    private float rotationY = 0.0f;
    void Start()
    {
        //lock mouse to center of screen
        Cursor.lockState = CursorLockMode.Locked;
        CarEnterHandle.onEnter.AddListener(ResetCameraRotation);
    }

    void ResetCameraRotation(ulong clientId)
    {
        Debug.Log("Reset camera rotation");
        transform.localEulerAngles = new Vector3(0, 0, 0);
        rotationY = 0;
    }

    void Update()
    {
        // Rotate the camera based on the mouse movement
        rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;

        rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        //limit camera rotation to the left
        if (rotationX < maximumX && rotationX > maximumX - 10)
        {
            rotationX = maximumX;
        }
        else if (rotationX > minimumX && rotationX < minimumX + 10)
        {
            rotationX = minimumX;
        }

        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0.0f);
    
        LookOverShoulder();
        
    }

    void LookOverShoulder()
    {
        if(rotationX < minimumX)
        {
            positionOffset = rotationX * cameraTruckAdjustment * positionOffsetRightFraction;
        }
        else if(rotationX > maximumX)
        {
            positionOffset = (360 - rotationX) * cameraTruckAdjustment * -1 * positionOffsetLeftFraction;
        }


        transform.localPosition = new Vector3(positionOffset, 0, 0);
    }
}
