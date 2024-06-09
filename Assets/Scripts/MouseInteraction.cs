using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

//Script to handle mouse interaction with objects
//Should be attached to the camera
public class MouseInteraction : NetworkBehaviour
{
    private Camera cam;
    public RaycastHit hit;

    public PlayerController playerController;

    void Start()
    {
        cam = GetComponent<Camera>();
        if(!IsOwner)
        {
            cam.enabled = false;
        }
    }

    public override void OnNetworkSpawn()
    {
        playerController = PlayerController.GetPlayerPlayerController(OwnerClientId);
    }
    
    void Update()
    {
        if(!IsOwner)
        {
            return;
        }
        //raycast from camera
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        
        
        if (Physics.Raycast(ray, out hit, 3))
        {
            if(hit.collider.gameObject.GetComponent<HoverInfo>())
            {
                hit.collider.gameObject.GetComponent<HoverInfo>().Show();
            }

            if(hit.collider.gameObject.GetComponent<Pickup>())
            {   
                if(Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponent<Pickup>().PickupItemRpc(playerController.GetComponent<Inventory>());
                }
                
            }

            if (hit.collider.gameObject.tag == "Radio")
            {
                if(Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponent<Radio>().ToggleRpc();
                }
                
            }
            if(hit.collider.gameObject.GetComponent<UpgradeButton>())
            {
                if(Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponent<UpgradeButton>().Upgrade();
                }
                
            }
            if (hit.collider.gameObject.GetComponent<CarExitHandle>())
            {
                if(Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponent<CarExitHandle>().Exit(NetworkManager.Singleton.LocalClientId);
                }
                
            }
            if (hit.collider.gameObject.GetComponent<CarEnterHandle>())
            {
                if(Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponent<CarEnterHandle>().Enter(NetworkManager.Singleton.LocalClientId);
                }
                
            }
        }
    }
}
