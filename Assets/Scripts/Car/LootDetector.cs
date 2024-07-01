using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LootDetector : MonoBehaviour
{
    GameObject closestLootItem = null;
    [SerializeField] float maxDetectionDistance = 50f;
    [SerializeField] private float maxBeepTime = 4f;

    [SerializeField] private GameObject DashLight;

    float beepDelay = 1;
    float timer = 0;

    void Start()
    {
        InvokeRepeating(nameof(DetectNearestLoot), 0, 1f);
        InvokeRepeating(nameof(UpdateBeepRate), 0, 0.1f);
        DashLight.SetActive(false);
    }

    void FixedUpdate()
    {
        if(closestLootItem == null)
        {
            DashLight.SetActive(false);
            return;
        }

        timer += Time.fixedDeltaTime;
        if(timer >= beepDelay)
        {
            DashLight.SetActive(true);
            timer = 0;
        }
        else
        {
            DashLight.SetActive(false);
        }
    }


    void DetectNearestLoot()
    {
        float closestDistance = Mathf.Infinity;
        GameObject[] lootItems = GameObject.FindGameObjectsWithTag("Loot");
        foreach (var lootItem in lootItems)
        {
            if(closestDistance > Vector3.Distance(transform.position, lootItem.transform.position))
            {
                closestDistance = Vector3.Distance(transform.position, lootItem.transform.position);
                closestLootItem = lootItem;
                
            }
        }
    }

    void UpdateBeepRate()
    {
        if(closestLootItem != null)
        {
            float distance = Vector3.Distance(transform.position, closestLootItem.transform.position);
            beepDelay = distance / maxDetectionDistance * maxBeepTime;
            beepDelay = Mathf.Clamp(beepDelay, 0, maxBeepTime);
        }
        else
        {
            beepDelay = 0;
        }
    }
}
