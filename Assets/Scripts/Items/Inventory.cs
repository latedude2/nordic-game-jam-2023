using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Inventory : NetworkBehaviour
{
    [SerializeField] private ItemList GameItemList;

    private List<ItemReference> items;

    [SerializeField] List<ItemReference> startingItems;

    public ItemReference EquippedItem;

    GameObject EquippedItemGameObject;
    [SerializeField] public Transform RightHand;

    void Awake()
    {
        items = new List<ItemReference>();
        foreach (var item in startingItems)
        {
            items.Add(item);
        }
    }

    [Rpc(SendTo.Owner)]
    public void AddItemRpc(string item)
    {
        items.Add(JsonUtility.FromJson<ItemReference>(item));
        EquipItemRpc(item);
    }

    [Rpc(SendTo.Owner)]
    public void RemoveItemRpc(string item)
    {
        items.Remove(JsonUtility.FromJson<ItemReference>(item));
    }

    [Rpc(SendTo.Everyone)]
    public void EquipItemRpc(string item)
    {
        EquippedItem = JsonUtility.FromJson<ItemReference>(item);

        if (EquippedItemGameObject != null)
        {
            Destroy(EquippedItemGameObject);
        }

       
        EquippedItemGameObject = Instantiate(GameItemList.FindItem(EquippedItem).EquippedItemPrefab, RightHand);
        Debug.Log("Equipped: " + EquippedItemGameObject.name);

        //TODO?: Convert item from JSON to object
        //TODO: Show equipped item in UI (for owner)
        //TODO?: Spawn equipped item in hand
        //TODO: Play equip sound - in spawned item?
        //TODO: Play equip animation - animation could be found on equipped item prefab
        //TODO: Give item functionality - scripts on the spawned item
        //TODO: Update item stats
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
            EquipItemRpc(JsonUtility.ToJson(items[index]));
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
            EquipItemRpc(JsonUtility.ToJson(items[index]));
        }
        

        //Debug log inventory content
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Inventory:");
            foreach (var item in items)
            {
                Debug.Log(item.ItemName + " " + item.AmountLeft);
            }
        }
    }
}
