using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverInfoUpgrade : HoverInfo
{
    override public string GetDisplayString()
    {
        if(GetComponent<UpgradeButton>().CurrentUpgradeLevel >= 5)
        {
            return "Upgrade " + GetComponent<UpgradeButton>().upgradeType.ToString() + " is maxed out";
        }
        return "Upgrade " + GetComponent<UpgradeButton>().upgradeType.ToString() + " to level " + (GetComponent<UpgradeButton>().CurrentUpgradeLevel + 1);
    }
}
