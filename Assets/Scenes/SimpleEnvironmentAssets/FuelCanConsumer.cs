using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCanConsumer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FuelCan>())
        {
            Destroy(other.gameObject);
            FindObjectOfType<CarFuelResource>().Refuel();
            GetComponent<AudioSource>().Play();
        }
    }
}
