using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class DeliveryCounter : MonoBehaviour
{
    private Text counterText;
    private int deliveries = 0;
    void Start()
    {
        //ObjectiveManager.Instance.OnObjectiveCompleted.AddListener(UpdateCounter);
        counterText = GetComponent<Text>();
        counterText.text = "";
    }

    void UpdateCounter()
    {
        deliveries++;
        counterText.text = deliveries.ToString() + "/" + ObjectiveManager.Instance.RequiredPizzasForExit.ToString();
    }
}
