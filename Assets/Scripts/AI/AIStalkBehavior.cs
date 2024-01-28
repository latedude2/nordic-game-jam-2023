using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using VLB;
using System;

public class AIStalkBehavior : MonoBehaviour
{

    GameObject player;
    [NonSerialized] public float Intensity = 1;
    private float stalkDistance = 10f;

    public float defaultStalkDistance = 70f;
    private Vector3 stalkPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnEnable()
    {
        stalkDistance = defaultStalkDistance;
        ModifyBehaviorAccordingToIntensity();
        SetCurrentPlayerGameobject();
        SetDestination();
        StartCoroutine(nameof(UpdateDestination));
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

    private IEnumerator UpdateDestination()
    {
        while (true)
        {
            if (!isActiveAndEnabled)
            {
                yield break;
            }
            yield return new WaitForSeconds(ChaseIntervalScaledToIntensity());  
            if (!isActiveAndEnabled)
            {
                yield break;
            }
            SetCurrentPlayerGameobject();
            if(Vector3.Distance(transform.position, player.transform.position) > stalkDistance)
            {
                Debug.Log("Stalking player because they are too far away");
                SetDestination();
            }
            else if (Vector3.Distance(transform.position, stalkPos) < 10f)
            {
                Debug.Log("Stalking player monster reached stalk position");
                SetDestination();
            }
                
            yield return null;
        }
    }

    private void SetDestination()
    {
        Vector3 playerPos = player.transform.position;
        Vector2 stalk2D = UnityEngine.Random.insideUnitCircle * stalkDistance;
        stalkPos = playerPos +  new Vector3(stalk2D.x, 0, stalk2D.y);

        if(RandomPoint(playerPos, stalkDistance, out stalkPos))
        {
            GetComponent<NavMeshAgent>().SetDestination(stalkPos);
        }
        else
        {
            Debug.Log("Stalking player failed to find a random point after 30 attempts, setting to aggressive chase");
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
        stalkDistance = defaultStalkDistance / Intensity;
    }

    float ChaseIntervalScaledToIntensity()
    {
        //The agent becomes more aggressive the higher the intensity starting with intensity 1
        //The agent will attack the player more often

        return Math.Max(0.5f, 7f - Intensity);
    }
}
