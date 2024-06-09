using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Radio : NetworkBehaviour
{
    [SerializeField] private AudioClip knobSound;
    [SerializeField] private AudioSource knobAudioSource;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private List<AudioClip> musicClips;
    [SerializeField] private Transform radioLight;

    public RadioController radioController;
    int currentlyPlaying = 0;
    

    void Update()
    {
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


    public void ReactToRadioSwitch(bool oldState, bool isOn)
    {
        knobAudioSource.PlayOneShot(knobSound);
        radioLight.gameObject.SetActive(isOn);
        if (isOn)
        {
            GetComponent<AudioSource>().volume = 1;
        }
        else
        {
            GetComponent<AudioSource>().volume = 0;
        }
    }

    public void Toggle()
    {
        radioController.ToggleRpc();
    }
}
