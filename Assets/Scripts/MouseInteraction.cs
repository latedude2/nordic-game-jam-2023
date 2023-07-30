using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteraction : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //raycast to see if mouse is over object
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Radio")
            {
                Debug.Log("Mouse over player");
                if(Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponent<Radio>().Toggle();
                }
                
            }
            if (hit.collider.gameObject.tag == "DoorHandle")
            {
                Debug.Log("Mouse over Handle");
                if(Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponent<DoorHandle>().Exit();
                }
                
            }
        }
    }
}
