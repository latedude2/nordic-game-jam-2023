using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera camera;
    private Vector3 cameraPosition;
    [SerializeField] private float cameraSpeed = 1000f;
    void Start()
    {
        camera = GetComponent<Camera>();
        cameraPosition = camera.transform.parent.localPosition;
        //lock mouse
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Rotate camera using mouse 
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.RotateAround(transform.position, Vector3.up, mouseX * cameraSpeed * Time.deltaTime);
        transform.RotateAround(transform.position, transform.right, -mouseY * cameraSpeed * Time.deltaTime);

        //stop rotation on the z axis
        Vector3 euler = transform.eulerAngles;
        euler.z = 0;
        transform.eulerAngles = euler;

        //move camera
        float moveX = transform.localRotation.eulerAngles.y;
        //
  
        transform.parent.localPosition = cameraPosition + new Vector3(1,0,0) * moveX * 0.01f;



        
    }

    
}
