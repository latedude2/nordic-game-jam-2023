using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;

public class PostProcessingControl : MonoBehaviour
{
    Volume volume;
    
    void Start()
    {
        //ObjectiveManager.Instance.OnObjectiveCompleted.AddListener(AdjustPostProcessing);
        volume = GetComponent<Volume>();
        volume.weight = 0.05f;
    }


    void AdjustPostProcessing()
    {
        volume.weight += 0.05f;
    }
}
