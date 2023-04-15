using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringSound : MonoBehaviour
{
    
    //make sound when spring is activated
    public List<AudioClip> springSound;
    private AudioSource audioSource;
    private WheelCollider wheelCollider;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        wheelCollider = GetComponent<WheelCollider>();
    }

    void Update()
    {
        WheelHit hit;
        if (wheelCollider.GetGroundHit(out hit))
        {
            if (hit.force > 10000)
            {
                Debug.Log("Spring triggered");
                audioSource.clip = springSound[Random.Range(0, springSound.Count)];
                //audioSource.volume = hit.force / 20000;
                audioSource.Play();
            }
        }
    }
}
