using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PizzaPlaceExit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            int Day = PlayerPrefs.GetInt("Day");
            PlayerPrefs.SetInt("Day", Day + 1);
            SceneManager.LoadScene("MainScene");
        }
    }
}
