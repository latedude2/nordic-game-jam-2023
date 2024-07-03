using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Scriptable object to store item data to send over the network
[CreateAssetMenu(fileName = "ItemList", menuName = "ScriptableObjects/ItemList", order = 1)]
public class ItemList : ScriptableObject
{
    public List<Item> Items = new List<Item>();


    public Item FindItem(ItemReference itemReference)
    {
        foreach (var item in Items)
        {
            if (item.itemReference.Equals(itemReference))
            {
                return item;
            }
        }
        Debug.LogError("Item not found in ItemList: " + itemReference.ItemName);
        return null;
    }
}
