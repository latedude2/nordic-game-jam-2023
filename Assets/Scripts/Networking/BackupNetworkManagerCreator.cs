#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using ParrelSync;

public class BackupNetworkManagerCreator : MonoBehaviour
{
    public GameObject NetworkManagerPrefab; 
    // Start is called before the first frame update
    void Start()
    {
        // Check if there is a network manager in the scene
        if (FindObjectOfType<NetworkManager>() == null)
        {
            // If not, create one
            GameObject NetworkManager = Instantiate(NetworkManagerPrefab);
            
            //looks like we want to start a level while developing. Start host and client depending if this is a cloned version of the editor.
            if (ClonesManager.IsClone())
            {
                NetworkManager.GetComponent<NetworkManager>().StartClient();
            }
            else 
            {
                NetworkManager.GetComponent<NetworkManager>().StartHost();
            }
        }

        Destroy(gameObject);
    }

}

#endif