using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Unity.VisualScripting;

public class CarExitHandle : MonoBehaviour
{
    //event for exiting the car
    static public UnityEvent onExit;
    [SerializeField] private Transform playerSpawnLocation; 
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] GameObject rainEffect;

    AudioSource audioSource;
    
    static public CarExitHandle Instance;
    public AudioClip carExitSound;

    public void Awake()
    {
        Instance = this;
        onExit = new UnityEvent();
    }

    public void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Exit()
    {
        audioSource.PlayOneShot(carExitSound);
        onExit.Invoke();
        //point player forward but dont tilt
        Vector3 rotation = playerSpawnLocation.rotation.eulerAngles;
        rotation.x = 0;
        rotation.z = 0;
        playerSpawnLocation.rotation = Quaternion.Euler(rotation);
        GameObject player = Instantiate(playerPrefab, playerSpawnLocation.position, playerSpawnLocation.rotation);
        if(rainEffect != null)
            rainEffect.transform.SetParent(player.transform);
    }
}
