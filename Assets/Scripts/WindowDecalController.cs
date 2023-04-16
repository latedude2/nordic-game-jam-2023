using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowDecalController : MonoBehaviour
{
    float activeSeconds = 0f;
    Color colorHidden;
    Color colorShown;
    Renderer rend;
    float lerpVal = 0;
    public float fadeInSpeed = 0f;
    public float fadeOutSpeed = 0f;
    void Start()
    {
        rend = GetComponent<Renderer>();
        colorHidden = new Color(1,1,1,0);
        colorShown = new Color(1,1,1,.6f);
    }

    public void SetActiveSeconds(float time){
        activeSeconds = time;
    }

    void Update()
    {
        if (activeSeconds > 0){
            activeSeconds -= Time.deltaTime;
            if (lerpVal < 1){
                lerpVal += Time.deltaTime * fadeInSpeed;
            } else {
                lerpVal = 1;
            }
        } else {
            if (lerpVal > 0){
                lerpVal -= Time.deltaTime * fadeOutSpeed;
            } else {
                lerpVal = 0;
            }
        }

        rend.material.color = Color.Lerp(colorHidden, colorShown, lerpVal);
    }
}
