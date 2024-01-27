using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class CarExitHandle : MonoBehaviour
{
    //event for exiting the car
    static public UnityEvent onExit;
    [SerializeField] private Transform playerSpawnLocation; 
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] GameObject rainEffect;

    public void Awake()
    {
        onExit = new UnityEvent();
    }

    public void Exit()
    {
        Debug.Log("Exit car");
        onExit.Invoke();
        //point player forward but dont tilt
        Vector3 rotation = playerSpawnLocation.rotation.eulerAngles;
        rotation.x = 0;
        rotation.z = 0;
        playerSpawnLocation.rotation = Quaternion.Euler(rotation);
        GameObject player = Instantiate(playerPrefab, playerSpawnLocation.position, playerSpawnLocation.rotation);
        rainEffect.transform.SetParent(player.transform);
    }
}
