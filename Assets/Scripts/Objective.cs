using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Objective : NetworkBehaviour
{
    public AudioClip finishSound;
    private AudioSource audioSource;

    public bool isCompleted = false;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        
        if(!IsHost)
        {
            return;
        }

        if(isCompleted)
        {
            return;
        }

        if(other.gameObject.GetComponent<ObjectiveCompleter>() != null)
        {
            ObjectiveManager.Instance.ObjectiveCompleted();
            audioSource.PlayOneShot(finishSound);
        }
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, Terrain.activeTerrain.transform.position.y + Terrain.activeTerrain.SampleHeight(transform.position) + 0.5f, transform.position.z);
    }

    [Rpc(SendTo.Everyone)]
    public void SetObjectiveActiveRpc(bool enabled)
    {
        gameObject.SetActive(enabled);  //not sure if this works yet. Do inactive gameobjects receive RPCs? 
                                        //if not, we can just use a NetworkVariable<bool> instead

    }
    
}
