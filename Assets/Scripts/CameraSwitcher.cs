using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] bool enableCameraOnCarEnter = true;
    static public UnityEvent onEnter;
    static public UnityEvent onExit;

    void Start()
    {
        onExit = new UnityEvent();
        onEnter = new UnityEvent();
        if(CarEnterHandle.onEnter != null)
        {
            CarEnterHandle.onEnter.AddListener(OnCarEnter);
            CarExitHandle.onExit.AddListener(OnCarExit);
        }
    }

    void OnCarEnter()
    {
        if(!enableCameraOnCarEnter)
            Destroy(gameObject);
        else 
        {
            Debug.Log("Enter car");
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        onEnter.Invoke();
    }

    void OnCarExit()
    {
        Debug.Log("Exit car");
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(!enableCameraOnCarEnter);
        }
        onExit.Invoke();
    }
}
