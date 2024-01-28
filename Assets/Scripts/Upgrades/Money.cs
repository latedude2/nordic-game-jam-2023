using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Money : MonoBehaviour
{
    public int money = 0;
    public static Money Instance;
    public UnityEvent OnMoneySpent;
    public UnityEvent OnMoneyAdded;

    private AudioSource audioSource;

    public AudioClip moneyAddedSound;
    public AudioClip moneySpentSound;
    

void Awake()
    {
        Instance = this;
        OnMoneySpent = new UnityEvent();
        OnMoneyAdded = new UnityEvent();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Start()
    {
        money = PlayerPrefs.GetInt("Money", 0);    
        if(ObjectiveManager.Instance != null)
            ObjectiveManager.Instance.OnObjectiveCompleted.AddListener(ObjectiveCompletionReward);
    }

    private void ObjectiveCompletionReward()
    {
        AddMoney(100);
    }

    public void AddMoney(int amount)
    {
        money += amount;
        audioSource.PlayOneShot(moneyAddedSound);
        PlayerPrefs.SetInt("Money", money);
        OnMoneyAdded.Invoke();
    }

    public void RemoveMoney(int amount)
    {
        money -= amount;
        audioSource.PlayOneShot(moneySpentSound);
        PlayerPrefs.SetInt("Money", money);
        OnMoneySpent.Invoke();
    }



}
