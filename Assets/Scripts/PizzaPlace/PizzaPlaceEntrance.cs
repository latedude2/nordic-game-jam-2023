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
            Debug.Log("Collision with player on trigger");
            if(ObjectiveManager.Instance.GetCompletedObjectiveCount() > 4)
            {
                SceneManager.LoadScene("PizzaPlaceInside");
            }
            else
            {
                Debug.Log("Not enough objectives completed");
            }
           
        }
    }
}
