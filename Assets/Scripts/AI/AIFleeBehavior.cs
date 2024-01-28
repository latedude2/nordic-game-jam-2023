using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using VLB;
using System;

public class AIFleeBehavior : MonoBehaviour
{

    GameObject player;
    [NonSerialized] public float Intensity = 1;
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
        ModifyBehaviorAccordingToIntensity();
        SetDestination();
        UpdateDestination();
    }

    private void SetCurrentPlayerGameobject()
    {
        GameObject playerWalking = GameObject.Find("Human(Clone)");
        if(playerWalking != null)
        {
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
        Invoke(nameof(IfStillRunningSetToAggressive), 20f);

        
    }

    void IfStillRunningSetToAggressive()
    {
        if (!isActiveAndEnabled)
        {
            return;
        }
        Debug.Log("Fleeing for too long, setting to aggressive");
        GetComponent<AIBehaviorChooser>().SetAIAggressive();
    }

    private void SetDestination()
    {
        SetCurrentPlayerGameobject();
        Vector3 playerPos = player.transform.position;
        Vector3 fleePos = playerPos;

        if(RandomPoint(playerPos, fleeDistance, out fleePos))
        {
            GetComponent<NavMeshAgent>().SetDestination(fleePos);
        }
        else
        {
            Debug.Log("Stalking player failed to find a random point, setting to aggressive chase");
            GetComponent<AIBehaviorChooser>().SetAIAggressive();
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result) {
		for (int i = 0; i < 30; i++) {
            Vector2 stalk2D = UnityEngine.Random.insideUnitCircle * range;
			Vector3 randomPoint = center + new Vector3(stalk2D.x, 0, stalk2D.y);
			NavMeshHit hit;
			if (NavMesh.SamplePosition(randomPoint, out hit, 5.0f, NavMesh.AllAreas)) {
				result = hit.position;
				return true;
			}
		}
		result = Vector3.zero;
		return false;
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
