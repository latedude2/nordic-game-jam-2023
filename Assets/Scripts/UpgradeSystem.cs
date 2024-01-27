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
    [NonSerialized] public int SteeringUpgrade = 0;

    // Start is called before the first frame update
    void Start()
    {
        AccelerationUpgrade = PlayerPrefs.GetInt("AccelerationUpgrade", 0);
        MaxSpeedUpgrade = PlayerPrefs.GetInt("MaxSpeedUpgrade", 0);
        brakeForceUpgrade = PlayerPrefs.GetInt("brakeForceUpgrade", 0);
        SteeringUpgrade = PlayerPrefs.GetInt("SteeringUpgrade", 0);
    }
}
