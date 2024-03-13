using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

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
            Instantiate(NetworkManagerPrefab);
        }

        Destroy(gameObject);
    }

}
