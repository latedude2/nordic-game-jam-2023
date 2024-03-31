using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Unity.VisualScripting;
using Unity.Netcode;

public class ObjectiveManager : NetworkBehaviour
{
    public List<Objective> potentialObjectives;
    private float terrainIncreaseValue = 0.0002f;
    private NetworkVariable<int> completedObjectiveCount = new NetworkVariable<int>(0);

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
    }

    
    public override void OnNetworkSpawn()
    {
        Invoke(nameof(SetupObjectives), 1f);
    }

    public void SetupObjectives()
    {
        foreach(Objective objective in potentialObjectives)
        {
            objective.SetObjectiveActiveRpc(false);
        }
        currentObjective = potentialObjectives[0];
        currentObjective.SetObjectiveActiveRpc(true);
    }

    void Update()
    {
        if(!IsHost)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Using cheat to increase completed objective count");
            completedObjectiveCount.Value++;
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
        //make current objective visible TODO: run on clients
        currentObjective.SetObjectiveActiveRpc(true);
        currentObjective.isCompleted = false;
        oldObjective.isCompleted = true;
        StartCoroutine(SetObjectiveNotVisible(oldObjective));
        //increase terrain target Todo: figure out how to keep in sync across network
        //TerrainController.Instance.SetAmplitudeTargetAndSpeed(TerrainController.Instance.amplitudeTarget + terrainIncreaseValue, 0.000001f);
        
        completedObjectiveCount.Value++;
        PlayerPrefs.SetInt("NewScore", completedObjectiveCount.Value);
        if (completedObjectiveCount.Value > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", completedObjectiveCount.Value);
        }
    }

    IEnumerator SetObjectiveNotVisible(Objective objective)
    {
        //Start thank you animation
        yield return new WaitForSeconds(5f);
        currentObjective.SetObjectiveActiveRpc(false);
        yield return null;
    }

    public int GetCompletedObjectiveCount()
    {
        return completedObjectiveCount.Value;
    }

    public bool IsEnoughPizzaDelivered()
    {
        return completedObjectiveCount.Value >= RequiredPizzasForExit;
    }

    public int GetRemainingPizzasForExit()
    {
        return RequiredPizzasForExit - completedObjectiveCount.Value;
    }
}
