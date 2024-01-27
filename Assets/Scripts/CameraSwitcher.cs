using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] bool enableCameraOnCarEnter = true;


    void Start()
    {
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
    }

    void OnCarExit()
    {
        Debug.Log("Exit car");
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(!enableCameraOnCarEnter);
        }
    }
}
