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
            Invoke(nameof(LoadScene), 5f);
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("EndScreen");
    }
}
