using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFlasher : MonoBehaviour
{
    Renderer rend;
    Color color;
    [SerializeField] float rate;
    [SerializeField] float offset;
    float i;

    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        i += Time.deltaTime*rate;
        color = new Color(0.9433962f, 0.8425186f, 0.3070486f, Mathf.Lerp(0.035f ,0.13f, Mathf.PingPong((i * 2) + offset, 1)));
        rend.material.SetColor("_Color", color);
    }
}
