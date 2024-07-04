using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class FuelCanConsumer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FuelCan>())
        {
            if(NetworkManager.Singleton.IsServer)
            {
                other.gameObject.GetComponent<NetworkObject>().Despawn(true);
                FindObjectOfType<CarFuelResource>().RefuelRpc();
            }
            GetComponent<AudioSource>().Play();
        }
    }
}
