using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Unity.Netcode;

public class CameraSwitcher : NetworkBehaviour
{
    [SerializeField] bool enableCameraOnCarEnter = true;
    static public UnityEvent onEnter;
    static public UnityEvent onExit;

    static public bool playerInCar = false;

    void Start()
    {
        onExit = new UnityEvent();
        onEnter = new UnityEvent();
        if(CarEnterHandle.onEnter != null)
        {
            CarEnterHandle.onEnter.AddListener(OnCarEnter);
            CarExitHandle.onExit.AddListener(OnCarExit);
        }
    }

    void OnCarEnter(ulong clientId)
    {
        if(GetComponentInParent<NetworkObject>() == null)
        {
            return;
        }
        playerInCar = true;
        if(!enableCameraOnCarEnter)
        {
            if(GetComponentInParent<NetworkObject>().OwnerClientId == clientId)
                RequestDespawnRpc();
        }
        else 
        {
            GetComponentInChildren<Camera>().enabled = true;
            GetComponentInChildren<MouseInteraction>().enabled = true;
            GetComponentInChildren<CameraControl>().enabled = true;
            RequestCarPosessRpc(clientId);
            Debug.Log("RequestCarPosessRpc for client " + NetworkManager.Singleton.LocalClientId.ToString());
        }
        onEnter.Invoke();
    }

    void OnCarExit()
    {
        playerInCar = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(!enableCameraOnCarEnter);
        }
        onExit.Invoke();
    }

    [Rpc(SendTo.Server)]
    public void RequestDespawnRpc()
    {
        gameObject.GetComponent<NetworkObject>().Despawn();
    }

    [Rpc(SendTo.Server)]
    public void RequestCarPosessRpc(ulong ClientId)
    {
        Debug.Log("Received request RequestCarPosessRpc on server for client " + ClientId.ToString());
        GetComponentInParent<PrometeoCarController>().Possess(ClientId);
        foreach (Transform child in transform)
        {
            if(child.GetComponent<Possessable>() != null)
                child.GetComponent<Possessable>().Possess(ClientId);
        }
    }
}
