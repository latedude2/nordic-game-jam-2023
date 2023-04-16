using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    float timeLeft = 60f;
    public Text timerText;
    [SerializeField] private AudioClip timerSound;
    [SerializeField] private AudioClip bombSound;
    private AudioSource audioSource;

    static public bool ded = false;

    void Start()
    {   
        audioSource = gameObject.AddComponent<AudioSource>();
        ObjectiveManager.Instance.OnObjectiveCompleted.AddListener(IncreaseTime);
        InvokeRepeating(nameof(PlayTimerSound), 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        //
        string minutes = Mathf.Floor(timeLeft / 60).ToString("00");
        string seconds = Mathf.Floor(timeLeft % 60).ToString("00");
        timerText.text = minutes + ":" + seconds;

        if(timeLeft < -5)
        {
            SceneManager.LoadScene("EndScreen");
        }
        
        if(timeLeft < 0)
        {
            GameObject blackScreen = GameObject.Find("BlackScreen");
            blackScreen.GetComponent<Image>().enabled = true;
            if (!ded)
            {
                ded = true;
                //stop all audio sources in scene
                AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
                foreach (AudioSource audioSource in audioSources)
                {
                    audioSource.Stop();
                    audioSource.enabled = false;
                }
                audioSource.enabled = true;
                audioSource.PlayOneShot(bombSound);
            }
        }
    }
    void PlayTimerSound()
    {
        if (timeLeft > 0 && timeLeft < 10)
        {
            audioSource.PlayOneShot(timerSound);
        }
    }

    void IncreaseTime()
    {
        timeLeft += 50f;
    }
}
