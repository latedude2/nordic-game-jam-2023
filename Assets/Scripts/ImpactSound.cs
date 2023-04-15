using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSound : MonoBehaviour
{
    
    public AudioClip[] impactSounds;
    private AudioSource audioSource;
    private float playInterval = 0.5f;
    private float lastPlayTime = 0;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (Time.time - lastPlayTime < playInterval)
        {
            return;
        }
        lastPlayTime = Time.time;
        audioSource.PlayOneShot(impactSounds[Random.Range(0, impactSounds.Length)]);
    }

}
