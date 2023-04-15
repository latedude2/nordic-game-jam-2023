using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePointer : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //rotate to point at objective
        transform.LookAt(ObjectiveManager.Instance.currentObjective.transform);
    }
}
