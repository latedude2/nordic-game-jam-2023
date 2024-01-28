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
        GetComponent<AIFleeBehavior>().enabled = false;
        Debug.Log("Setting AI to aggressive");
    }

    public void SetAIStalk()
    {
        //set AIStalkBehavior to active
        GetComponent<AIStalkBehavior>().enabled = true;
        GetComponent<AIAttackBehavior>().enabled = false;
        GetComponent<AIFleeBehavior>().enabled = false;
        Debug.Log("Setting AI to stalk");
    }

    public void SetAIFlee()
    {
        //set AIFleeBehavior to active
        GetComponent<AIFleeBehavior>().enabled = true;
        GetComponent<AIAttackBehavior>().enabled = false;
        GetComponent<AIStalkBehavior>().enabled = false;
        Debug.Log("Setting AI to flee");
    }
}
