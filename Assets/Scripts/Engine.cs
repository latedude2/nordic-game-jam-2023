using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Unity.Netcode;

public class Engine : NetworkBehaviour
{
    public NetworkVariable<bool> isOn = new NetworkVariable<bool>(false);
    public static Engine Instance;
    Rigidbody carRigidbody;
    float engineTimer = 0;
    AudioSource engineTurnSoundSource;
    [SerializeField] private AudioMixerGroup engineMixer;
    public AudioClip engineStartSound;
    public AudioClip engineStopSound;
    public Transform leftLight;
    public Transform rightLight;
    public Transform interiorLight;
    public Transform tutorialPrompt;
    public float engineStartTime =  3f;
    private bool carReachedProperSpeed = false;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        engineTurnSoundSource = gameObject.AddComponent<AudioSource>();
        engineTurnSoundSource.outputAudioMixerGroup = engineMixer;
        leftLight.gameObject.SetActive(false);
        rightLight.gameObject.SetActive(false);
        interiorLight.gameObject.SetActive(false);
    }

    public override void OnNetworkSpawn()
    {
        isOn.OnValueChanged += ReactToEngineSwitch;
    }

    public override void OnNetworkDespawn()
    {
        isOn.OnValueChanged -= ReactToEngineSwitch;
    }

    // Update is called once per frame
    void Update()
    {   
        if(GetComponent<PrometeoCarController>().OwnerClientId != NetworkManager.Singleton.LocalClientId)
        {
            return;
        }
        if(isOn.Value)
        {
            /* functionality for engine randomly turning off
            if(carRigidbody.velocity.magnitude > 1f)
            {
                carReachedProperSpeed = true;
            }
            //Random chance to turn off if velocity is low
            if (Random.Range(0, 100) == 0 && carRigidbody.velocity.magnitude < 0.5f && carReachedProperSpeed)
            {
                TurnOffRpc();
            }
            */
            return;
        }
        
        //Hold to turn on engine
        if (Input.GetKey(KeyCode.E))
        {
            engineTimer += Time.deltaTime;
            if (!engineTurnSoundSource.isPlaying)
            {
                StartEngineSoundRpc();
            }
        }
        else 
        {
            CancelStartingEngineSoundRpc();
        }
        if(engineTimer > engineStartTime)
        {
            TurnOnRpc();
        }
    }

    [Rpc(SendTo.Everyone)]
    void StartEngineSoundRpc()
    {
        engineTurnSoundSource.clip = engineStartSound;
        engineTurnSoundSource.Play();
    }

    [Rpc(SendTo.Everyone)]
    void CancelStartingEngineSoundRpc()
    {
        if(engineTurnSoundSource.clip == engineStartSound)
            engineTurnSoundSource.Stop();
        engineTimer = 0;
    }

    [Rpc(SendTo.Server)]
    void TurnOffRpc()
    {
        isOn.Value = false;
    }

    [Rpc(SendTo.Server)]
    void TurnOnRpc()
    {
        isOn.Value = true;
    }

    void ReactToEngineSwitch(bool oldState, bool isOn)
    {
        leftLight.gameObject.SetActive(isOn);
        rightLight.gameObject.SetActive(isOn);
        interiorLight.gameObject.SetActive(isOn);
        if(isOn)
        {   
            tutorialPrompt.gameObject.SetActive(false);
        }
        else
        {
            engineTurnSoundSource.clip = engineStopSound;
            engineTurnSoundSource.Play();
            carReachedProperSpeed = false;
        }
    }
}
