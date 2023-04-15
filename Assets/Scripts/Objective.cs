using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private Collider objectiveCollider;
    // Start is called before the first frame update
    void Start()
    {
        objectiveCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.gameObject.tag == "Player")
        {
            ObjectiveManager.Instance.ObjectiveCompleted();
        }
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,  Terrain.activeTerrain.SampleHeight(transform.position) + 0.5f, transform.position.z);
    }
    
}
