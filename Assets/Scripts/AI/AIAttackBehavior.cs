using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using VLB;
using System;

public class AIAttackBehavior : MonoBehaviour
{

    GameObject player;
    public float Intensity = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnEnable()
    {
        StartCoroutine(nameof(UpdateDestination));
    }

    private IEnumerator UpdateDestination()
    {
        while (true)
        {
            if (!isActiveAndEnabled)
            {
                yield break;
            }
            Debug.Log("Waiting for " + ChaseIntervalScaledToIntensity() + " seconds");
            yield return new WaitForSeconds(ChaseIntervalScaledToIntensity());  
            //get Player by tag
            GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
            Debug.Log("Navigating to player: " + player.transform.position);
            yield return null;
        }
    }

    public void ModifyBehaviorAccordingToIntensity()
    {
        //The agent becomes more aggressive the higher the intensity starting with intensity 1
        //The agent will attack the player more often
        GetComponent<NavMeshAgent>().speed = 2 * Intensity;
        GetComponent<NavMeshAgent>().acceleration = 8 * Intensity;
        GetComponent<NavMeshAgent>().angularSpeed = 120 * Intensity;
    }

    float ChaseIntervalScaledToIntensity()
    {
        //The agent becomes more aggressive the higher the intensity starting with intensity 1
        //The agent will attack the player more often
    
        return Math.Max(0.5f, 10f - Intensity);
    }
}
