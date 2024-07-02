using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class ImpactSound : MonoBehaviour
{
    
    public AudioClip[] impactSounds;
    private AudioSource audioSource;
    private float playInterval = 0.5f;
    private float lastPlayTime = 0;

    [SerializeField]
    private ShakePreset shakePreset;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.3f;
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            return;
        }
        if(collider.gameObject.tag == "Finish")
        {
            return;
        }
        
        if (Time.time - lastPlayTime < playInterval)
        {
            return;
        }
        lastPlayTime = Time.time;
        Shaker.ShakeAll(shakePreset);
        audioSource.PlayOneShot(impactSounds[Random.Range(0, impactSounds.Length)]);
    }

}
