using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Pushable : NetworkBehaviour
{

    public Rigidbody pushableRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        pushableRigidbody = RecursiveGetRigidbody(transform);
    }

    [Rpc(SendTo.Owner)]
    public void PushRpc(Vector3 force)
    {
        pushableRigidbody.AddForce(force, ForceMode.Impulse);
    }

    Rigidbody RecursiveGetRigidbody(Transform t)
    {
        if (t.GetComponent<Rigidbody>() != null)
        {
            return t.GetComponent<Rigidbody>();
        }
        else if(t.parent != null)
        {
            return RecursiveGetRigidbody(t.parent);
        }
        else return null;
    }
}
