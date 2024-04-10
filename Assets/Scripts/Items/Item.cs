using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Item : NetworkBehaviour
{
    public GameObject DroppedPickupPrefab;
    public GameObject EquippedItemPrefab;
    public string ItemName;
    public string ItemDescription;
    public int AmountLeft;
}
