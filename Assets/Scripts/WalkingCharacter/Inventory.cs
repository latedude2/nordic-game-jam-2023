using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Inventory : NetworkBehaviour
{
    private List<NetworkBehaviourReference> items;

    public NetworkBehaviourReference EquippedItem;

    [Rpc(SendTo.Owner)]
    public void AddItemRpc(NetworkBehaviourReference item)
    {
        items.Add(item);
        EquipItemRpc(item);
    }

    [Rpc(SendTo.Owner)]
    public void RemoveItemRpc(NetworkBehaviourReference item)
    {
        items.Remove(item);
    }

    [Rpc(SendTo.Owner)]
    public void EquipItemRpc(NetworkBehaviourReference item)
    {
        EquippedItem = item;
    }

    void Update()
    {
        if(!IsOwner)
        {
            return;
        }
        //when scrolling mouse, change equipped item
        if (Input.mouseScrollDelta.y > 0)
        {
            int index = items.IndexOf(EquippedItem);
            if (index == items.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            EquipItemRpc(items[index]);
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            int index = items.IndexOf(EquippedItem);
            if (index == 0)
            {
                index = items.Count - 1;
            }
            else
            {
                index--;
            }
            EquipItemRpc(items[index]);
        }
        

        //Debug log inventory content
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Inventory:");
            foreach (var item in items)
            {
                item.TryGet(out Item itemInstance);
                Debug.Log(itemInstance.ItemName);
            }
        }
    }
}
