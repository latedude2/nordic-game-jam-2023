using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverInfoPizzaEnter : HoverInfo
{
    override public string GetDisplayString()
    {
        if(ObjectiveManager.Instance == null || ObjectiveManager.Instance.IsEnoughPizzaDelivered())
        {
            return "Finish the day";
        }
        else
        {
            return "You need to deliver " + ObjectiveManager.Instance.GetRemainingPizzasForExit() + " more pizzas to before you can return";
        }
    }
}
