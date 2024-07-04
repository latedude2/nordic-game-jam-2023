using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class FuelCanInHand : MonoBehaviour
{
    MouseInteraction mouseInteraction;
    void Start()
    {
        mouseInteraction = transform.parent.GetComponent<ReferenceToCamera>().mouseInteraction;
        
    }
    void Update()
    {
        if(!mouseInteraction.IsOwner)
        {
            return;
        }

        if(!Input.GetMouseButtonDown(0))
        {
            return;
        }

        if (mouseInteraction.hit.collider == null)
        {
            return;
        }

        Debug.Log(mouseInteraction.hit.collider.name);

        Collider[] colliders = Physics.OverlapSphere(mouseInteraction.hit.point, 0.1f);

        foreach (var collider in colliders)
        {
            if (collider.GetComponent<FuelCanConsumer>())
            {
                FindObjectOfType<CarFuelResource>().RefuelRpc();

                PlayerController.GetPlayerPlayerController(mouseInteraction.OwnerClientId).GetComponent<Inventory>().SpendItemRpc("Fuel Can");
            }
        }

    }
}
