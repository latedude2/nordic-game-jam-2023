using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using VLB;
using System;

public class AIStalkBehavior : MonoBehaviour
{

    GameObject player;
    public float Intensity = 1;
    private float stalkDistance = 10f;

    public float defaultStalkDistance = 70f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnEnable()
    {
        SetDestination();
        StartCoroutine(nameof(UpdateDestination));
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
            if (!isActiveAndEnabled)
            {
                yield break;
            }
            SetDestination();
            yield return null;
        }
    }

    private void SetDestination()
    {
        SetCurrentPlayerGameobject();
        Vector3 playerPos = player.transform.position;
        Vector2 stalk2D = UnityEngine.Random.insideUnitCircle * stalkDistance;
        Vector3 stalkPos = playerPos +  new Vector3(stalk2D.x, 0, stalk2D.y);

        //get Player by tag
        GetComponent<NavMeshAgent>().SetDestination(stalkPos);
        //Debug.Log("Stalking player at : " + stalkPos + " player position: " + player.transform.position);
    }

    public void ModifyBehaviorAccordingToIntensity()
    {
        //The agent becomes more aggressive the higher the intensity starting with intensity 1
        //The agent will attack the player more often
        GetComponent<NavMeshAgent>().speed = 3 * Intensity;
        GetComponent<NavMeshAgent>().acceleration = 8 * Intensity;
        GetComponent<NavMeshAgent>().angularSpeed = 120 * Intensity;
        stalkDistance = defaultStalkDistance / Intensity;
    }

    float ChaseIntervalScaledToIntensity()
    {
        //The agent becomes more aggressive the higher the intensity starting with intensity 1
        //The agent will attack the player more often

        return Math.Max(0.5f, 4f - Intensity);
    }
}
