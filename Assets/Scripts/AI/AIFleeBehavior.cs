using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using VLB;
using System;

public class AIFleeBehavior : MonoBehaviour
{

    GameObject player;
    public float Intensity = 1;
    private float fleeDistance = 10f;

    public float defaultFleeDistance = 200f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(nameof(FleeUntilReached));
    }

    IEnumerator FleeUntilReached()
    {
        while (true)
        {
            if (!isActiveAndEnabled)
            {
                yield break;
            }
            yield return new WaitForSeconds(1);  
            if (!isActiveAndEnabled)
            {
                yield break;
            }
            if(Vector3.Distance(transform.position, player.transform.position) < CalculateFleeDistance())
            {
                GetComponent<AIBehaviorChooser>().SetAIAggressive();
                yield break;
            }
            yield return null;
        }
    }

    void OnEnable()
    {
        SetDestination();
        UpdateDestination();
    }

    private void SetCurrentPlayerGameobject()
    {
        GameObject playerWalking = GameObject.Find("Human");
        if(playerWalking != null)
        {
            Debug.Log("Player walking found");
            player = playerWalking;
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void UpdateDestination()
    {
        if (!isActiveAndEnabled)
        {
            return;
        }
        SetDestination();

        
    }

    private void SetDestination()
    {
        SetCurrentPlayerGameobject();
        Vector3 playerPos = player.transform.position;
        Vector2 stalk2D = UnityEngine.Random.insideUnitCircle * fleeDistance;
        Vector3 fleePos = playerPos +  new Vector3(stalk2D.x, 0, stalk2D.y);

        //get Player by tag
        GetComponent<NavMeshAgent>().SetDestination(fleePos);
        
    }

    public void ModifyBehaviorAccordingToIntensity()
    {
        //The agent becomes more aggressive the higher the intensity starting with intensity 1
        //The agent will attack the player more often
        GetComponent<NavMeshAgent>().speed = 2 * Intensity;
        GetComponent<NavMeshAgent>().acceleration = 8 * Intensity;
        GetComponent<NavMeshAgent>().angularSpeed = 120 * Intensity;
        fleeDistance = CalculateFleeDistance();
    }

    float CalculateFleeDistance()
    {
        return defaultFleeDistance / Intensity;
    }
}
