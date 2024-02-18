using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Unity.Netcode;

public class CarSeat : NetworkBehaviour
{
    [SerializeField] bool isDriverSeat = false;
    public Camera camera;
    static public UnityEvent onEnter;
    static public UnityEvent onExit;

    static public bool playerInCar = false;
    static public ulong drivingClientId = 99;

    void Start()
    {
        onExit = new UnityEvent();
        onEnter = new UnityEvent();
        if(CarExitHandle.onExit != null)
        {
            CarExitHandle.onExit.AddListener(OnCarExit);
        }
        camera.enabled = false;
        GetComponentInChildren<AudioListener>().enabled = false;
        GetComponentInChildren<MouseInteraction>().enabled = false;
        GetComponentInChildren<CameraControl>().enabled = false;
        
    }

    public IEnumerator DelayedCameraEnable()
    {
        yield return new WaitForEndOfFrame();
        camera.enabled = true;
    }

    public void CarEnter(ulong clientId)
    {
        if(GetComponentInParent<NetworkObject>() == null)
        {
            return;
        }
        playerInCar = true;

        StartCoroutine(DelayedCameraEnable());
        GetComponentInChildren<AudioListener>().enabled = true;
        GetComponentInChildren<MouseInteraction>().enabled = true;
        GetComponentInChildren<CameraControl>().enabled = true;
        if(isDriverSeat)
        {
            RequestCarPosessRpc(clientId);
            Debug.Log("RequestCarPosessRpc for client " + NetworkManager.Singleton.LocalClientId.ToString());
        }  

        RequestCameraPosessRpc(clientId);
    
        onEnter.Invoke();
    }

    void OnCarExit()
    {
        playerInCar = false;
        camera.enabled = false;
        GetComponentInChildren<AudioListener>().enabled = false;
        GetComponentInChildren<MouseInteraction>().enabled = false;
        GetComponentInChildren<CameraControl>().enabled = false;
        if(isDriverSeat)
            RequestCarUnPosessRpc();
        
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
    }

    [Rpc(SendTo.Server)]
    public void RequestCameraPosessRpc(ulong ClientId)
    {
        GetComponentInChildren<NetworkObject>().ChangeOwnership(ClientId);
    }

    [Rpc(SendTo.Server)]
    public void RequestCarUnPosessRpc()
    {
        GetComponentInParent<PrometeoCarController>().Unpossess();
    }
}
