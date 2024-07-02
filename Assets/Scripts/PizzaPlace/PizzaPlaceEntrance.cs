using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PizzaPlaceEntrance : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(ObjectiveManager.Instance.IsEnoughPizzaDelivered())
            {
                SceneManager.LoadScene("PizzaPlaceInside");
            }
        }
    }
}
