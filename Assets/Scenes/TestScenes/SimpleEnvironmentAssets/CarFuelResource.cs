using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PrometeoCarController))]
public class CarFuelResource : MonoBehaviour
{
    public RectTransform fuelDisplay;
    private float fuelDisplayMaxWidth;
    private float fuelDisplayMaxHeight;
    private Vector3 previousPosition;
    public float maxFuel = 1.0f;
    public float startingFuel = 0.0f;
    public float fuelSpendScalar;
    public float currentFuel;
    private Engine engine;

    void Start()
    {
        previousPosition = transform.position;
        currentFuel = startingFuel;
        engine = GetComponent<Engine>();
        fuelDisplayMaxWidth = fuelDisplay.sizeDelta.x;
        fuelDisplayMaxHeight = fuelDisplay.sizeDelta.y;
    }

    void Update()
    {
        SpendFuel();
        EvaluateFuel();
        UpdateFuelDisplay();
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

    private void UpdateFuelDisplay()
    {
        fuelDisplay.sizeDelta = new Vector2(currentFuel * fuelDisplayMaxWidth, fuelDisplayMaxHeight);
    }
}
