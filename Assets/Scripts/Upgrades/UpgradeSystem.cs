using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    [NonSerialized] public int AccelerationUpgrade = 0;
    [NonSerialized] public int MaxSpeedUpgrade = 0;
    [NonSerialized] public int brakeForceUpgrade = 0;
    [NonSerialized] public int SteeringUpgrade = 1;

    // Start is called before the first frame update
    void Start()
    {
        /* disable upgrades for now
        AccelerationUpgrade = PlayerPrefs.GetInt("AccelerationUpgrade", 0);
        MaxSpeedUpgrade = PlayerPrefs.GetInt("MaxSpeedUpgrade", 0) * 10;
        brakeForceUpgrade = PlayerPrefs.GetInt("BrakeForceUpgrade", 0) * 50;
        SteeringUpgrade = PlayerPrefs.GetInt("SteeringUpgrade", 1) * 3;
        */
    }
}
