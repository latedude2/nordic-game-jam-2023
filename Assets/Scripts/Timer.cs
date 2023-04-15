using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    float timeLeft = 60f;
    
    void Start()
    {   
        ObjectiveManager.Instance.OnObjectiveCompleted.AddListener(IncreaseTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IncreaseTime()
    {
        timeLeft += 15f;
    }
}
