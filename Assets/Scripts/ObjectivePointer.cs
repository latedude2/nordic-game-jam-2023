using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePointer : MonoBehaviour
{
    Transform Exit;

    void Start()
    {
        Exit = GameObject.Find("PizzaPlaceEnterCollider").transform;
    }
    // Update is called once per frame
    void Update()
    {
        if(ObjectiveManager.Instance == null)
            return;

        if(ObjectiveManager.Instance.IsEnoughPizzaDelivered())
        {
            transform.LookAt(Exit);
        }
        //rotate to point at objective
        else if(ObjectiveManager.Instance.currentObjective != null)
            transform.LookAt(ObjectiveManager.Instance.currentObjective.transform);
    }
}
