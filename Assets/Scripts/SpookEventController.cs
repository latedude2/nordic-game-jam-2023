using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpookEventController : MonoBehaviour
{
    public int spookOmeter = 0;
    float timePassed = 0f;

    [SerializeField] GameObject[] decals;

    void Start()
    {
        ObjectiveManager.Instance.OnObjectiveCompleted.AddListener(IncreaseSpookLikelyhood);
    }

    void IncreaseSpookLikelyhood(){
        spookOmeter++;
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed > 5f)
        {
            timePassed -= Random.Range(5f, 15f);
            if(Random.Range(0,100/(spookOmeter+1)) <= 3){
                float time = Random.Range(.5f,3+(spookOmeter/2));
                decals[Random.Range(0,decals.Length)].GetComponent<WindowDecalController>().SetActiveSeconds(time);
            }
        } 
    }
}
