using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class RadioController : NetworkBehaviour
{
    [SerializeField] public Radio radio;
    public NetworkVariable<bool> isOn = new NetworkVariable<bool>(false);

    public override void OnNetworkSpawn()
    {
        Invoke(nameof(DelayedSpawn), 1);
    }

    void DelayedSpawn()
    {
        radio.radioController = this;
        radio.GetComponent<AudioSource>().Play();
        if(IsServer)
            InvokeRepeating(nameof(RandomToggle), 25, 15);
        isOn.OnValueChanged += radio.ReactToRadioSwitch;
    }

    public override void OnNetworkDespawn()
    {
        if(IsServer)
            CancelInvoke(nameof(RandomToggle));
        isOn.OnValueChanged -= radio.ReactToRadioSwitch;
    }

    void Update()
    {
        if(!IsOwner)
            return;
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
}
