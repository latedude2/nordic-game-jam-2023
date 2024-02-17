using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Unity.Netcode;

public class CameraSwitcher : MonoBehaviour
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

    void OnCarEnter()
    {
        if(GetComponentInParent<NetworkObject>() != null && !GetComponentInParent<NetworkObject>().IsOwner)
        {
            return;
        }
        playerInCar = true;
        if(!enableCameraOnCarEnter)
        {
            gameObject.GetComponent<NetworkObject>().Despawn();
        }
        else 
        {
            GetComponentInChildren<Camera>().enabled = true;
            GetComponentInChildren<MouseInteraction>().enabled = true;
            GetComponentInChildren<CameraControl>().enabled = true;
            foreach (Transform child in transform)
            {
                if(child.GetComponent<Possessable>() != null)
                    child.GetComponent<Possessable>().Possess(NetworkManager.Singleton.LocalClientId);
            }
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
}
