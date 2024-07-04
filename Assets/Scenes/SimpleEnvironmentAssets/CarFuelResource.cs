using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(PrometeoCarController))]
public class CarFuelResource : NetworkBehaviour
{
    public RectTransform fuelDisplay;
    private float fuelDisplayMaxWidth;
    private float fuelDisplayMaxHeight;
    private Vector3 previousPosition;
    public float maxFuel = 1.0f;
    public float startingFuel = 0.0f;
    public float fuelSpendScalar;

    //current fuel networked float 
    public NetworkVariable<float> currentFuel = new NetworkVariable<float>(0.0f);
   
    private Engine engine;

    void Start()
    {
        engine = GetComponent<Engine>();
        fuelDisplayMaxWidth = fuelDisplay.sizeDelta.x;
        fuelDisplayMaxHeight = fuelDisplay.sizeDelta.y;
        UpdateFuelDisplay(0, startingFuel);
        previousPosition = transform.position;        
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            currentFuel.Value = startingFuel;
        }
        currentFuel.OnValueChanged += UpdateFuelDisplay;
    }

    void Update()
    {
        if(!IsServer || !IsSpawned)
        {
            return;
        }
        SpendFuel();
        EvaluateFuel();
    }

    private void SpendFuel()
    {
        if (engine.isOn.Value)
        {
            currentFuel.Value -= fuelSpendScalar * Vector3.Distance(transform.position, previousPosition);
        }
        previousPosition = transform.position;
    }

    private void EvaluateFuel()
    {
        if (currentFuel.Value <= 0)
        {
            if (engine.isOn.Value)
            {
                engine.TurnOffRpc();
            }
        }
    }

    [Rpc(SendTo.Server)]
    public void RefuelRpc()
    {
        currentFuel.Value = maxFuel;
    }

    private void UpdateFuelDisplay(float oldFuelAmount, float newFuelAmount)
    {
        fuelDisplay.sizeDelta = new Vector2(newFuelAmount/maxFuel * fuelDisplayMaxWidth, fuelDisplayMaxHeight);
    }
}
