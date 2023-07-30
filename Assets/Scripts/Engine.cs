using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Engine : MonoBehaviour
{
    public bool isOn = false;
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

    // Update is called once per frame
    void Update()
    {   
        if(isOn)
        {
            if(carRigidbody.velocity.magnitude > 1f)
            {
                carReachedProperSpeed = true;
            }
            //Random chance to turn off if velocity is low
            if (Random.Range(0, 100) == 0 && carRigidbody.velocity.magnitude < 0.5f && carReachedProperSpeed)
            {
                TurnOff();
            }
            return;
        }
        //Hold to turn on engine
        if (GetComponent<PrometeoCarController>().playerControlled && Input.GetKey(KeyCode.E))
        {
            engineTimer += Time.deltaTime;
            if (!engineTurnSoundSource.isPlaying)
            {
                engineTurnSoundSource.clip = engineStartSound;
                engineTurnSoundSource.Play();
            }
        }
        else 
        {
            if(engineTurnSoundSource.clip == engineStartSound)
                engineTurnSoundSource.Stop();
            engineTimer = 0;
        }
        if(engineTimer > engineStartTime)
        {
            TurnOn();
        }
    }

    void TurnOff()
    {
        isOn = false;
        engineTurnSoundSource.clip = engineStopSound;
        engineTurnSoundSource.Play();
        leftLight.gameObject.SetActive(false);
        rightLight.gameObject.SetActive(false);
        interiorLight.gameObject.SetActive(false);
        carReachedProperSpeed = false;
    }

    void TurnOn()
    {
        isOn = true;
        leftLight.gameObject.SetActive(true);
        rightLight.gameObject.SetActive(true);
        interiorLight.gameObject.SetActive(true);
        tutorialPrompt.gameObject.SetActive(false);
    }
}
