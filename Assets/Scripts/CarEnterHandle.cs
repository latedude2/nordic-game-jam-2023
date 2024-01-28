using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class CarEnterHandle : MonoBehaviour
{
    //event for exiting the car
    static public UnityEvent onEnter;
    [SerializeField] GameObject rainEffect;
    [SerializeField] GameObject car;

    AudioSource audioSource;

    public AudioClip carEnterSound;

    public void Awake()
    {
        onEnter = new UnityEvent();
    }
    public void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Enter()
    {
        audioSource.PlayOneShot(carEnterSound);
        onEnter.Invoke();
        if(rainEffect != null)
            rainEffect.transform.SetParent(car.transform);
    }
}
