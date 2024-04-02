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
    int currentlyPlaying = 0;
    public NetworkVariable<bool> isOn = new NetworkVariable<bool>(true);

    public override void OnNetworkSpawn()
    {
        Invoke(nameof(DelayedSpawn), 1);
    }

    void DelayedSpawn()
    {
        GetComponent<AudioSource>().Play();
        if(IsServer)
            InvokeRepeating(nameof(RandomToggle), 25, 15);
        isOn.OnValueChanged += ReactToRadioSwitch;
    }


    public override void OnNetworkDespawn()
    {
        if(IsServer)
            CancelInvoke(nameof(RandomToggle));
        isOn.OnValueChanged -= ReactToRadioSwitch;
    }

    void Update()
    {
        if(!Engine.Instance.isOn.Value)
        {
            if(isOn.Value)
                TurnOffRpc();
            return;
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            ToggleRpc();
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
            if(!isOn.Value)
                ToggleRpc();
        }
    }

    [Rpc(SendTo.Server)]
    public void ToggleRpc()
    {
        isOn.Value = !isOn.Value;
    }

    [Rpc(SendTo.Server)]
    private void TurnOffRpc()
    {
        isOn.Value = false;
    }

    void ReactToRadioSwitch(bool oldState, bool isOn)
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
}
