using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSound : MonoBehaviour
{
    
    public AudioClip[] impactSounds;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collider)
    {
        audioSource.PlayOneShot(impactSounds[Random.Range(0, impactSounds.Length)]);
    }

}
