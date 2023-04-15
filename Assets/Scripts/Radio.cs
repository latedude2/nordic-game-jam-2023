using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] private AudioClip knobSound;
    [SerializeField] private AudioSource knobAudioSource;
    [SerializeField] private AudioSource musicAudioSource;
    bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        //Randombly toggle radio
        InvokeRepeating(nameof(RandomToggle), 25, 15);
        GetComponent<AudioSource>().Play();
    }

    private void RandomToggle()
    {
        if (Random.Range(0, 3) == 0)
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        knobAudioSource.PlayOneShot(knobSound);
        isOn = !isOn;
        if (isOn)
        {
            //set volume to 1
            GetComponent<AudioSource>().volume = 1;
        }
        else
        {
            GetComponent<AudioSource>().volume = 0;
        }
    }
}
