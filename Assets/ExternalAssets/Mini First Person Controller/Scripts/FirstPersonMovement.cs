using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FirstPersonMovement : NetworkBehaviour, Possessable
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody ControllerRigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    [SerializeField] Animator animator;


    void Awake()
    {
        // Get the rigidbody on this.
        ControllerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(!IsOwner)
        {
            return;
        }
        
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        if(targetVelocity.magnitude > 0.1f)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        // Apply movement.
        ControllerRigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, ControllerRigidbody.velocity.y, targetVelocity.y);
    }

    public void Possess(ulong clientID)
    {
        Debug.Log("Possessing" + gameObject.name);
    }

    public void Unpossess()
    {
        Debug.Log("Unpossessing" + gameObject.name);
    }
}