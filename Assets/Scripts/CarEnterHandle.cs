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

    public void Awake()
    {
        onEnter = new UnityEvent();
    }

    public void Enter()
    {
        Debug.Log("Enter car");
        onEnter.Invoke();
        rainEffect.transform.SetParent(car.transform);
    }
}
