using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Unity.VisualScripting;

public class ObjectiveManager : MonoBehaviour
{
    public List<Objective> potentialObjectives;
    private float terrainIncreaseValue = 0.0002f;
    private int completedObjectiveCount = 0;

    public int RequiredPizzasForExit = 4; 

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
        RequiredPizzasForExit = RequiredPizzasForExit + PlayerPrefs.GetInt("Day");
        OnObjectiveCompleted.AddListener(UpdateIntensities);


        foreach(Objective objective in potentialObjectives)
        {
            objective.gameObject.SetActive(false);
        }
        currentObjective = potentialObjectives[0];
        currentObjective.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Using cheat to increase completed objective count");
            completedObjectiveCount++;
            OnObjectiveCompleted.Invoke();
        }
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
        currentObjective.isCompleted = false;
        oldObjective.isCompleted = true;
        StartCoroutine(SetObjectiveNotVisible(oldObjective));
        //increase terrain target
        TerrainController.Instance.SetAmplitudeTargetAndSpeed(TerrainController.Instance.amplitudeTarget + terrainIncreaseValue, 0.000001f);
        
        completedObjectiveCount++;
        PlayerPrefs.SetInt("NewScore", completedObjectiveCount);
        if (completedObjectiveCount > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", completedObjectiveCount);
        }
    }

    IEnumerator SetObjectiveNotVisible(Objective objective)
    {
        //Start thank you animation
        yield return new WaitForSeconds(5f);
        objective.gameObject.SetActive(false);
        yield return null;
    }

    public int GetCompletedObjectiveCount()
    {
        return completedObjectiveCount;
    }

    private void UpdateIntensities()
    {
        //Find monsters
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach(GameObject monster in monsters)
        {
            monster.GetComponent<AIAttackBehavior>().ChaseIntensity = 1 + completedObjectiveCount;
            monster.GetComponent<AIStalkBehavior>().Intensity = 1 + completedObjectiveCount;
            monster.GetComponent<AIFleeBehavior>().Intensity = 1 + completedObjectiveCount;
        }
    }

    public bool IsEnoughPizzaDelivered()
    {
        return completedObjectiveCount >= RequiredPizzasForExit;
    }

    public int GetRemainingPizzasForExit()
    {
        return RequiredPizzasForExit - completedObjectiveCount;
    }
}
