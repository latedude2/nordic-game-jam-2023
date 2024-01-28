using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health = 3;
    // Start is called before the first frame update

    public void TakeDamage(int damage)
    {
        //TODO: trigger damage sound and animation

        
        health -= damage;
        Debug.Log("Health: " + health);
        if(health <= 0)
        {
            GameObject blackScreen = GameObject.Find("BlackScreen");
            blackScreen.GetComponent<Image>().enabled = true;
            Invoke(nameof(EndGame), 5f);
        }
    }

    private void EndGame()
    {
        //reset upgrades
        PlayerPrefs.SetInt("AccelerationUpgrade", 0);
        PlayerPrefs.SetInt("MaxSpeedUpgrade", 0);
        PlayerPrefs.SetInt("BrakeForceUpgrade", 0);
        PlayerPrefs.SetInt("SteeringUpgrade", 1);
        PlayerPrefs.SetInt("Money", 0);
        PlayerPrefs.SetInt("Day", 0);

        

        SceneManager.LoadScene("EndScreen");
    }
}
