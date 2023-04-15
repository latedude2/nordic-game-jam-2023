using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
    private Vector3 originalPosition;
    private float positionChange = 0.01f;

    void Start()
    {
        originalPosition = transform.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = originalPosition + new Vector3(0, Mathf.Sin(Time.time * 2) * positionChange, 0);
    }
}
