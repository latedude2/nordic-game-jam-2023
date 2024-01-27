using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HoverInfoDisplay : MonoBehaviour
{
    private Text textDisplay;
    float lastTimeShown;

    public static HoverEvent onHover = new HoverEvent();
    void Start()
    {
        textDisplay = GetComponent<Text>();  
        onHover.AddListener(DisplayTextOnHover);
    }

    void Update()
    {
        if (Time.time - lastTimeShown > 0.1f)
        {
            textDisplay.text = "";
        }
    }
    
    [System.Serializable]
    public class HoverEvent : UnityEvent<string>
    {
    }

    void DisplayTextOnHover(string text)
    {
        lastTimeShown = Time.time;
        textDisplay.text = text;
    }

}
