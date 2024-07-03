using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Sirenix.OdinInspector;



//Scriptable object to store item data to send over the network

[System.Serializable]
public class ItemReference
{   
    [SerializeField] public string ItemName;
    [SerializeField] public string ItemDescription;
    [SerializeField] public int AmountLeft;

    //override comparison to use item name
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        return ItemName == ((ItemReference)obj).ItemName;
    }
    public override int GetHashCode()
    {
        return ItemName.GetHashCode();
    }
    public override string ToString()
    {
        return ItemName;
    }
}
