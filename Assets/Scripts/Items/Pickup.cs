using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Pickup : NetworkBehaviour
{
    public Item itemToGrantPlayer;

    [Rpc(SendTo.Owner)]
    public void PickupItemRpc(NetworkBehaviourReference Inventory)
    {
        // Grant the player the item
        if (Inventory.TryGet(out Inventory inventory))
        {

            inventory.AddItemRpc(JsonUtility.ToJson(itemToGrantPlayer.itemReference));
            // Destroy the pickup
            Destroy(gameObject);
        }
        else {
            Debug.Log("Pickup got reference to null inventory!");
        }
    }
}
