using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PrometeoCarController))]
public class CarFuelResource : MonoBehaviour
{
    private Vector3 previousPosition;
    public float maxFuel = 1.0f;
    public float fuelSpendScalar;
    public float currentFuel;
    private Engine engine;

    void Start()
    {
        previousPosition = transform.position;
        currentFuel = maxFuel;
        engine = GetComponent<Engine>();
    }

    void Update()
    {
        SpendFuel();
        EvaluateFuel();
    }

    private void SpendFuel()
    {
        currentFuel -= fuelSpendScalar * Vector3.Distance(transform.position, previousPosition);
        previousPosition = transform.position;
    }

    private void EvaluateFuel()
    {
        if (currentFuel <= 0)
        {
            engine.TurnOffRpc();
        }
    }

    public void Refuel()
    {
        currentFuel = maxFuel;
    }
}
