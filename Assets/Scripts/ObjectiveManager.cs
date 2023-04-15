using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public List<Objective> potentialObjectives;
    private Objective currentObjective;

    //singleton
    public static ObjectiveManager Instance;


    void Start()
    {
        Instance = this;
        foreach(Objective objective in potentialObjectives)
        {
            objective.gameObject.SetActive(false);
        }
        currentObjective = potentialObjectives[Random.Range(0, potentialObjectives.Count)];
        currentObjective.gameObject.SetActive(true);
    }

    public void ObjectiveCompleted()
    {
        Debug.Log("Objective completed");
        Objective oldObjective = currentObjective;
        potentialObjectives.Remove(oldObjective);
        currentObjective = potentialObjectives[Random.Range(0, potentialObjectives.Count)];
        potentialObjectives.Add(oldObjective);
        //make current objective visible
        currentObjective.gameObject.SetActive(true);
        //make old objective invisible
        oldObjective.gameObject.SetActive(false);
    }
}
