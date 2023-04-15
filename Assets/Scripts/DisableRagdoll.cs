using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRagdoll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //make all rigidbodies kinematic
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        //disable all colliders only in children

        Collider[] colliders = transform.GetChild(0).GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
