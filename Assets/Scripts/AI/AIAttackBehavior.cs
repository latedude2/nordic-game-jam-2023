using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using VLB;
using System;

public class AIAttackBehavior : MonoBehaviour
{

    GameObject player;
    [NonSerialized] public float ChaseIntensity = 1;

    public float DefaultChaseWait = 5f;

    public float DefaultAttackCooldown = 15f;
    private float AttackCooldown = 15f;
    public float ImpactForce = 3000000f;

    private SoundtrackController _soundtrackController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        AttackCooldown = AttackCooldownScaledToIntensity();
        CameraSwitcher.onEnter.AddListener(SetCurrentPlayerGameobject);
        CameraSwitcher.onExit.AddListener(SetCurrentPlayerGameobject);
    }

    void OnEnable()
    {
        SetCurrentPlayerGameobject();
        
        SetDestination(player.transform.position);
        StartCoroutine(nameof(UpdateDestination));

        _soundtrackController = FindObjectOfType<SoundtrackController>(); // TODO: Find a better way to do this
        if (_soundtrackController != null)
            _soundtrackController.ActivateThreat(gameObject, player);
    }

    private void OnDisable()
    {
        if (_soundtrackController != null)
            _soundtrackController.DeactivateThreat();
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
            yield return new WaitForSeconds(0.5f);  
            if (!isActiveAndEnabled)
            {
                yield break;
            }
            
            SetCurrentPlayerGameobject();
            Debug.Log("Running at player");
            SetDestination(player.transform.position);
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
        GetComponent<NavMeshAgent>().acceleration = 2 * ChaseIntensity;
        GetComponent<NavMeshAgent>().angularSpeed = 60 * ChaseIntensity;
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

        return ChaseIntensity * 500000;
    }

    void OnTriggerEnter(Collider other)
    {
        if(!enabled)
        {
            return;
        }
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Collision with player on trigger");
            Attack(other);
            GetComponent<AIBehaviorChooser>().SetAIFlee();
            
           
        }
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
