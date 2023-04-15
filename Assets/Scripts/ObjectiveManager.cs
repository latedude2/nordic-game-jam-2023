using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public List<Objective> potentialObjectives;
    private float terrainIncreaseValue = 0.0002f;
    private int completedObjectiveCount = 0;

    //event for when objective is completed
    public UnityEvent OnObjectiveCompleted;

    [System.NonSerialized] public Objective currentObjective;

    //singleton
    public static ObjectiveManager Instance;
    [SerializeField] private CharacterGenerator characterGenerator;


    void Awake()
    {
        Instance = this;
        OnObjectiveCompleted = new UnityEvent();
        PlayerPrefs.SetInt("NewScore", 0);
    }
    void Start()
    {
        
        foreach(Objective objective in potentialObjectives)
        {
            objective.gameObject.SetActive(false);
        }
        currentObjective = potentialObjectives[0];
        currentObjective.gameObject.SetActive(true);
    }

    public void ObjectiveCompleted()
    {
        OnObjectiveCompleted.Invoke();
        Objective oldObjective = currentObjective;
        potentialObjectives.Remove(oldObjective);
        currentObjective = potentialObjectives[Random.Range(0, potentialObjectives.Count)];
        potentialObjectives.Add(oldObjective);
        //make current objective visible
        currentObjective.gameObject.SetActive(true);
        //make old objective invisible
        oldObjective.gameObject.SetActive(false);
        //increase terrain target
        TerrainController.Instance.SetAmplitudeTargetAndSpeed(TerrainController.Instance.amplitudeTarget + terrainIncreaseValue, 0.000001f);
        
        completedObjectiveCount++;
        PlayerPrefs.SetInt("NewScore", completedObjectiveCount);
        if (completedObjectiveCount > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", completedObjectiveCount);
        }
    }
}
