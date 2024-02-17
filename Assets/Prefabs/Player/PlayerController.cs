using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : NetworkBehaviour
{
    public GameObject PossessedObject;
    public GameObject InitialPossessedPrefab;

    private Vector2 Look;
    private Vector2 Move;
    private bool Interact;
    private bool Jump;
    private bool Crouch;

    void Start()
    {
        if(IsServer)
        {
            PossessedObject = Instantiate(InitialPossessedPrefab);
            var instanceNetworkObject = PossessedObject.GetComponent<NetworkObject>();
            instanceNetworkObject.SpawnWithOwnership(OwnerClientId);
            PossessedObject.GetComponent<Possessable>().Possess();
        }
        
        if(IsOwner)
        {
            //lock mouse to center of screen
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
    public void OnLook(InputValue value)
    {
        Look = value.Get<Vector2>();
    }

    public void OnMove(InputValue value)
    {
        Move = value.Get<Vector2>();
    }

    public void OnInteract(InputValue value)
    {
        Interact = value.isPressed;
    }

    public void OnJump(InputValue value)
    {
        Jump = value.isPressed;
    }

    public void OnCrouch(InputValue value)
    {
        Crouch = value.isPressed;
    }

    

}
