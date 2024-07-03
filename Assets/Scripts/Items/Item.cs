using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Scriptable object to store item data to send over the network
[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    
    public GameObject DroppedPickupPrefab;
    public GameObject EquippedItemPrefab;
    public ItemReference itemReference;    


}
