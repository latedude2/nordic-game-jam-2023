using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to handle mouse interaction with objects
//Should be attached to the camera
public class MouseInteraction : MonoBehaviour
{
    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
    }
    
    void Update()
    {
        //raycast from camera
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3))
        {
            if(hit.collider.gameObject.GetComponent<HoverInfo>())
            {
                hit.collider.gameObject.GetComponent<HoverInfo>().Show();
            }

            if (hit.collider.gameObject.tag == "Radio")
            {
                Debug.Log("Mouse over player");
                if(Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponent<Radio>().Toggle();
                }
                
            }
            if (hit.collider.gameObject.GetComponent<CarExitHandle>())
            {
                Debug.Log("Mouse over Handle");
                if(Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponent<CarExitHandle>().Exit();
                }
                
            }
            if (hit.collider.gameObject.GetComponent<CarEnterHandle>())
            {
                Debug.Log("Mouse over Handle");
                if(Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponent<CarEnterHandle>().Enter();
                }
                
            }
        }
    }
}
