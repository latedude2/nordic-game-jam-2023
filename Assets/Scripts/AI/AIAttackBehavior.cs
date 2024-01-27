using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using VLB;
using System;

public class AIAttackBehavior : MonoBehaviour
{

    GameObject player;
    public float ChaseIntensity = 1;

    public float DefaultChaseWait = 5f;

    public float DefaultAttackCooldown = 15f;
    private float AttackCooldown = 15f;
    public float ImpactForce = 3000000f;
    bool recentlyAttacked = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        AttackCooldown = AttackCooldownScaledToIntensity();
    }

    void OnEnable()
    {
        SetCurrentPlayerGameobject();
        GameObject.Find("First Person Controller");
        
        SetDestination(player.transform.position);
        StartCoroutine(nameof(UpdateDestination));
    }

    private void SetCurrentPlayerGameobject()
    {
        GameObject playerWalking = GameObject.Find("First Person Controller");
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
            //get Player by tag
            SetDestination(player.transform.position);
            if (!isActiveAndEnabled)
            {
                yield break;
            }
            Debug.Log("Navigating to player at location: " + player.transform.position);
            yield return null;
        }
    }

    private void SetDestination(Vector3 destination)
    {
        GetComponent<NavMeshAgent>().SetDestination(destination);
    }

    public void ModifyBehaviorAccordingToIntensity()
    {
        //The agent becomes more aggressive the higher the intensity starting with intensity 1
        //The agent will attack the player more often
        GetComponent<NavMeshAgent>().speed = ChaseIntensity;
        GetComponent<NavMeshAgent>().acceleration = 4 * ChaseIntensity;
        GetComponent<NavMeshAgent>().angularSpeed = 60 * ChaseIntensity;
    }

    float ChaseIntervalScaledToIntensity()
    {
        //The agent becomes more aggressive the higher the intensity starting with intensity 1
        //The agent will attack the player more often
    
        return Math.Max(0.5f, DefaultChaseWait - ChaseIntensity * 0.5f);
    }

    float AttackCooldownScaledToIntensity()
    {
        //The agent becomes more aggressive the higher the intensity starting with intensity 1
        //The agent will attack the player more often

        return Math.Max(0.5f, DefaultAttackCooldown - ChaseIntensity);
    }

    float AttackForceScaledToIntensity()
    {
        //The agent becomes more aggressive the higher the intensity starting with intensity 1
        //The agent will attack the player more often

        return ChaseIntensity * 1000000;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Collision with player on trigger");

            //apply impulse to player
            if(!recentlyAttacked)
            {
                recentlyAttacked = true;
                Attack(other);
                StartCoroutine(ResetAttack());
            }
           
        }
    }

    IEnumerator ResetAttack()
    {
        GetComponent<AIBehaviorChooser>().SetAIStalk();
        yield return new WaitForSeconds(AttackCooldown);
        recentlyAttacked = false;
        GetComponent<AIBehaviorChooser>().SetAIAggressive();
    }

     void Attack(Collider collider)
    {
        if(collider.gameObject.GetComponentInParent<Rigidbody>() != null)
            collider.gameObject.GetComponentInParent<Rigidbody>().AddForce((collider.gameObject.transform.position - transform.position).normalized * AttackForceScaledToIntensity());
            
        if(collider.gameObject.GetComponentInParent<Health>() != null)
        {
            collider.gameObject.GetComponentInParent<Health>().TakeDamage(1);
        }   
    }
}
