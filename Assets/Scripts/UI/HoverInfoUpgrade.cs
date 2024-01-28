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
        else if(GetComponent<UpgradeButton>().upgradeCost > Money.Instance.money)
        {
            return "Not enough money to upgrade " + GetComponent<UpgradeButton>().upgradeType.ToString() + ". Upgrade costs " + GetComponent<UpgradeButton>().upgradeCost.ToString() + " and you have " + Money.Instance.money.ToString();
        }
        return "Upgrade " + GetComponent<UpgradeButton>().upgradeType.ToString() + " to level " + (GetComponent<UpgradeButton>().CurrentUpgradeLevel + 1);
    }
}
