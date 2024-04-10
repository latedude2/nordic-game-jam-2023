using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Pickup : NetworkBehaviour
{
    public NetworkBehaviour itemToGrantPlayer;

    [Rpc(SendTo.Owner)]
    public void PickupItemRpc(NetworkBehaviourReference Inventory)
    {
        // Grant the player the item
        if (Inventory.TryGet(out Inventory inventory))
        {
            inventory.AddItemRpc(itemToGrantPlayer);
            // Destroy the pickup
            Destroy(gameObject);
        }
        else {
            Debug.Log("Pickup got reference to null inventory!");
        }
        

        
    }
}
