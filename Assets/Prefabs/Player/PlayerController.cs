using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        //TODO: Try using OnLoadEventCompleted instead of OnLoadComplete
        NetworkManager.Singleton.SceneManager.OnLoadComplete += (clientID, sceneName, loadSceneMode) =>
        {
            if(clientID != OwnerClientId)
            {
                return;
            }
            
            //TODO: update this to be the actual scene name
            if(sceneName != "SimonasTestScene") return;

            if(IsHost)
            {
                PossessedObject = Instantiate(InitialPossessedPrefab);
                var instanceNetworkObject = PossessedObject.GetComponent<NetworkObject>();
                instanceNetworkObject.SpawnWithOwnership(OwnerClientId);
            }
            
            if(IsOwner)
            {
                //lock mouse to center of screen
                Cursor.lockState = CursorLockMode.Locked;
            }
        };
 
        
    }


    //Override when ownership is changed
    public override void OnGainedOwnership()
    {
        Debug.Log("Gained ownership of player controller");
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
