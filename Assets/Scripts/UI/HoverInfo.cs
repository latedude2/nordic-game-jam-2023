using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HoverInfo : MonoBehaviour
{
    public string TextToDisplay;

    void Start()
    {
       
    }

    public void Show()
    {
        Debug.Log("Mouse over " + gameObject.name);
        HoverInfoDisplay.onHover.Invoke(GetDisplayString());
    }

    virtual public string GetDisplayString()
    {
        return TextToDisplay;
    }

}
