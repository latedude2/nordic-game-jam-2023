using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnterWiggle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CarEnterHandle.onEnter.AddListener(ApplySidewaysImpulseToRigidbody);
    }

    void ApplySidewaysImpulseToRigidbody(ulong clientId, bool isDriverSeat){
        Rigidbody rb = GetComponent<Rigidbody>();
        //find point left of car
        Vector3 leftPoint = transform.position + transform.right * -10;
        rb.AddExplosionForce(50000, leftPoint, 100, -1);
    }
}
