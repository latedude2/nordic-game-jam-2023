using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Unity.VisualScripting;
using Unity.Netcode;

public class CarExitHandle : NetworkBehaviour
{
    //event for exiting the car
    static public UnityEvent<CarSeat> onExit;
    [SerializeField] private Transform playerSpawnLocation; 
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] GameObject rainEffect;

    AudioSource audioSource;
    
    static public CarExitHandle Instance;
    public AudioClip carExitSound;

    public void Awake()
    {
        Instance = this;
        onExit = new UnityEvent<CarSeat>();
    }

    public void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Exit(ulong ClientID, CarSeat carSeat)
    {
        audioSource.PlayOneShot(carExitSound);
        onExit.Invoke(carSeat);
        RequestSpawnCharacterRpc(ClientID);
        /*
        if(rainEffect != null)
            rainEffect.transform.SetParent(player.transform);
            */
    }

    [Rpc(SendTo.Server)]
    public void RequestSpawnCharacterRpc(ulong ClientID)
    {
        Vector3 rotation = playerSpawnLocation.rotation.eulerAngles;
        rotation.x = 0;
        rotation.z = 0;
        playerSpawnLocation.rotation = Quaternion.Euler(rotation);
        GameObject player = Instantiate(playerPrefab, playerSpawnLocation.position, playerSpawnLocation.rotation);
        var instanceNetworkObject = player.GetComponent<NetworkObject>();
        instanceNetworkObject.SpawnWithOwnership(ClientID);
    }
}
