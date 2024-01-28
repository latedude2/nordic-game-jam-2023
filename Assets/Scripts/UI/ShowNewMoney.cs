using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowNewMoney : MonoBehaviour
{
    
    void Start()
    {
        Money.Instance.OnMoneyAdded.AddListener(ShowNewMoneyText);
        Money.Instance.OnMoneySpent.AddListener(ShowNewMoneyText);
    }

    void ShowNewMoneyText()
    {
        GetComponent<Text>().text = "Money: " + Money.Instance.money.ToString();
        Invoke("ClearText", 4f);
    }

    void ClearText()
    {
        GetComponent<Text>().text = "";
    }
}
