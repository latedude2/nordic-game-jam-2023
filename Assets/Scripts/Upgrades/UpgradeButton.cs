using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{

    public enum UpgradeType
    {
        None,
        Acceleration,
        MaxSpeed,
        Brakes,
        Steering
    }

    public UpgradeType upgradeType;
    public int CurrentUpgradeLevel = 0;

    private int maxBrakeUpgradeLevel = 5;
    private int maxSteeringUpgradeLevel = 5;
    private int maxAccelerationUpgradeLevel = 5;
    private int maxMaxSpeedUpgradeLevel = 3;

    void Start()
    {
        CurrentUpgradeLevel = LoadCurrentUpgradeLevel();
    }

    int LoadCurrentUpgradeLevel()
    {
        if(upgradeType == UpgradeType.Acceleration)
        {
            return PlayerPrefs.GetInt("AccelerationUpgrade");
        }
        else if(upgradeType == UpgradeType.MaxSpeed)
        {
            return PlayerPrefs.GetInt("MaxSpeedUpgrade");
        }
        else if(upgradeType == UpgradeType.Brakes)
        {
            return PlayerPrefs.GetInt("BrakeForceUpgrade");
        }
        else if(upgradeType == UpgradeType.Steering)
        {
            return PlayerPrefs.GetInt("SteeringUpgrade");
        }
        return 0;
    }

    public void Upgrade()
    {
        if(upgradeType == UpgradeType.Acceleration)
        {
            if(CurrentUpgradeLevel >= maxAccelerationUpgradeLevel)
            {
                return;
            }
            CurrentUpgradeLevel++;
            PlayerPrefs.SetInt("AccelerationUpgrade", CurrentUpgradeLevel);
        }
        else if(upgradeType == UpgradeType.MaxSpeed)
        {
            if(CurrentUpgradeLevel >= maxMaxSpeedUpgradeLevel)
            {
                return;
            }
            CurrentUpgradeLevel++;
            PlayerPrefs.SetInt("MaxSpeedUpgrade", CurrentUpgradeLevel);
        }
        else if(upgradeType == UpgradeType.Brakes)
        {
            if(CurrentUpgradeLevel >= maxBrakeUpgradeLevel)
            {
                return;
            }
            CurrentUpgradeLevel++;
            PlayerPrefs.SetInt("BrakeForceUpgrade", CurrentUpgradeLevel);
        }
        else if(upgradeType == UpgradeType.Steering)
        {
            if(CurrentUpgradeLevel >= maxSteeringUpgradeLevel)
            {
                return;
            }
            CurrentUpgradeLevel++;
            PlayerPrefs.SetInt("SteeringUpgrade", CurrentUpgradeLevel);
        }
    }
}
