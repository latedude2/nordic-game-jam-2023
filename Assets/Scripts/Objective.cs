using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private Collider objectiveCollider;
    public AudioClip finishSound;
    private AudioSource audioSource;

    public bool isCompleted = false;
    void Start()
    {
        objectiveCollider = GetComponent<Collider>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
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

    
}
