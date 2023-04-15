using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintScores : MonoBehaviour
{
    public Text newScore;
    public Text highScore;
    // Start is called before the first frame update
    void Start()
    {
        //get scores from playerprefs
        int newScoreValue = PlayerPrefs.GetInt("NewScore");
        newScore.text = newScoreValue.ToString();
        int highScoreValue = PlayerPrefs.GetInt("HighScore");
        highScore.text = highScoreValue.ToString();
    }
}
