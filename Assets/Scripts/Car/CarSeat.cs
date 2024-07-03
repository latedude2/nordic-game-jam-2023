using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Unity.Netcode;
using Lightbug.GrabIt;

public class CarSeat : NetworkBehaviour
{
    [SerializeField] public bool isDriverSeat = false;
    public Camera SittingCamera;
    static public UnityEvent onEnter;
    static public UnityEvent<bool> onExit;

    static public ulong drivingClientId = 99;

    void Start()
    {
        onExit = new UnityEvent<bool>();
        onEnter = new UnityEvent();


        if(CarExitHandle.onExit != null)
        {
            CarExitHandle.onExit.AddListener(OnCarExit);
        }


        SittingCamera.enabled = false;
        GetComponentInChildren<AudioListener>().enabled = false;
        GetComponentInChildren<MouseInteraction>().enabled = false;
        GetComponentInChildren<CameraControl>().enabled = false;
        GetComponentInChildren<GrabIt>().enabled = false;
        GetComponentInChildren<Renderer>().enabled = false;
        
    }

    public IEnumerator DelayedCameraEnable()
    {
        yield return new WaitForEndOfFrame();
        SittingCamera.enabled = true;
    }

    public void CarEnter(ulong clientId)
    {
        StartCoroutine(DelayedCameraEnable());
        GetComponentInChildren<AudioListener>().enabled = true;
        GetComponentInChildren<MouseInteraction>().enabled = true;
        GetComponentInChildren<CameraControl>().enabled = true;
        GetComponentInChildren<GrabIt>().enabled = true;
        ShowPlayerInCarRpc(true);
        
        if(isDriverSeat)
        {
            RequestCarPosessRpc(clientId);
        }  

        RequestCameraPosessRpc(clientId);
    
        onEnter.Invoke();
    }

    void OnCarExit()
    {
        SittingCamera.enabled = false;
        GetComponentInChildren<AudioListener>().enabled = false;
        GetComponentInChildren<MouseInteraction>().enabled = false;
        GetComponentInChildren<CameraControl>().enabled = false;
        GetComponentInChildren<GrabIt>().enabled = false;
        ShowPlayerInCarRpc(false);
        
        if(isDriverSeat)
            RequestCarUnPosessRpc();
    }

    [Rpc(SendTo.Server)]
    public void RequestCarPosessRpc(ulong ClientId)
    {
        GetComponentInParent<PrometeoCarController>().Possess(ClientId);
    }

    [Rpc(SendTo.Everyone)]
    public void ShowPlayerInCarRpc(bool visible)
    {
        GetComponentInChildren<Renderer>().enabled = visible;
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
