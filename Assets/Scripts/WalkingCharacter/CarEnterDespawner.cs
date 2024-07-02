using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Unity.Netcode;

public class CarEnterDespawner : NetworkBehaviour
{
    static public UnityEvent onEnter;

    void Start()
    {
        onEnter = new UnityEvent();
        CarEnterHandle.onEnter.AddListener(CarEnter);
    }

    public void CarEnter(ulong clientId, bool ByDriver)
    {
        if(GetComponentInParent<NetworkObject>() == null)
        {
            return;
        }
        if(GetComponentInParent<NetworkObject>().OwnerClientId == clientId)
            RequestDespawnRpc();


        onEnter.Invoke();
    }

    [Rpc(SendTo.Server)]
    public void RequestDespawnRpc()
    {
        gameObject.GetComponent<NetworkObject>().Despawn();
    }
}
