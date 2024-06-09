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
    public Inventory Inventory;

    private Vector2 Look;
    private Vector2 Move;
    private bool Interact;
    private bool Jump;
    private bool Crouch;

    
    public override void OnNetworkSpawn()
    {
        #if !UNITY_EDITOR
        //TODO: Try using OnLoadEventCompleted instead of OnLoadComplete
        NetworkManager.Singleton.SceneManager.OnLoadComplete += (clientID, sceneName, loadSceneMode) =>
        {
            if(clientID != OwnerClientId)
            {
                return;
            }
            
            //TODO: update this to be the actual scene name
            if(sceneName != "MainScene") return;

            if(IsHost)
            {
                PossessedObject = Instantiate(InitialPossessedPrefab);
                PossessedObject.transform.position = GetSpawnPosition(OwnerClientId);
                var instanceNetworkObject = PossessedObject.GetComponent<NetworkObject>();
                instanceNetworkObject.SpawnWithOwnership(OwnerClientId);
            }
            
            if(IsOwner)
            {
                //lock mouse to center of screen
                Cursor.lockState = CursorLockMode.Locked;
            }
        };
        #endif

        #if UNITY_EDITOR
            Invoke(nameof(EditorSpawn), 1f);
        #endif
        
    }

    private void EditorSpawn()
    {
        if(SceneManager.GetActiveScene().name != "Lobby")
        {
            
            if(IsHost)
            {
                PossessedObject = Instantiate(InitialPossessedPrefab);
                PossessedObject.transform.position = GetSpawnPosition(OwnerClientId);
                var instanceNetworkObject = PossessedObject.GetComponent<NetworkObject>();
                instanceNetworkObject.SpawnWithOwnership(OwnerClientId);
            }
            
            if(IsOwner)
            {
                //lock mouse to center of screen
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    } 

    private Vector3 GetSpawnPosition(ulong clientID)
    {
        GameObject[] respawns = GameObject.FindGameObjectsWithTag("Respawn");
        if(respawns.Length == 0)
        {
            Debug.LogError("No spawn points found");
            return new Vector3(0, 0, 0);
        }

        if(respawns.Length <= (int)clientID)
        {
            Debug.LogError("Not enough spawn points for all players");
            return new Vector3(0, 0, 0);
        }

        return respawns[clientID].transform.position;
    }

    public static PlayerController GetPlayerPlayerController(ulong clientID)
    {
        foreach (var player in GameObject.FindGameObjectsWithTag("PlayerController"))
        {
            var playerController = player.GetComponent<PlayerController>();
            if(playerController.OwnerClientId == clientID)
            {
                return playerController;
            }
        }
        return null;
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
