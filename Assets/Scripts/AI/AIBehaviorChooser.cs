using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviorChooser : MonoBehaviour
{

    void Start()
    {
        SetAIStalk();
        ObjectiveManager.Instance.OnObjectiveCompleted.AddListener(UpdateAIBehavior);
    }

    // Update is called once per frame
    void UpdateAIBehavior()
    {
        if(ObjectiveManager.Instance.GetCompletedObjectiveCount() == 1)
        {
            SetAIStalk();
        }
        if (ObjectiveManager.Instance.GetCompletedObjectiveCount() > 1)
        {
            SetAIAggressive();
        }

    }

    

    public void SetAIAggressive()
    {
        //set AIAttackBehavior to active
        GetComponent<AIAttackBehavior>().enabled = true;
        GetComponent<AIStalkBehavior>().enabled = false;
        //make box red
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void SetAIStalk()
    {
        //set AIStalkBehavior to active
        GetComponent<AIStalkBehavior>().enabled = true;
        GetComponent<AIAttackBehavior>().enabled = false;
        //make box yellow
        GetComponent<Renderer>().material.color = Color.yellow;
    }
}
