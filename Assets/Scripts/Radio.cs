using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] private AudioClip knobSound;
    [SerializeField] private AudioSource knobAudioSource;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private List<AudioClip> musicClips;
    [SerializeField] private Transform radioLight;
    int currentlyPlaying = 0;
    bool isOn = true;
    // Start is called before the first frame update
    void Start()
    {
        //Randombly toggle radio
        InvokeRepeating(nameof(RandomToggle), 25, 15);
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Toggle();
        }
        //if music is not playing, play next song in list
        if (!musicAudioSource.isPlaying && !Timer.ded)
        {
            musicAudioSource.clip = musicClips[currentlyPlaying];
            musicAudioSource.Play();
            currentlyPlaying++;
            if (currentlyPlaying >= musicClips.Count)
            {
                currentlyPlaying = 0;
            }
        }
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
        radioLight.gameObject.SetActive(isOn);
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
