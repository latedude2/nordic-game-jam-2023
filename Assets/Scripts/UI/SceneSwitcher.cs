using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public void OpenMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void OpenGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
