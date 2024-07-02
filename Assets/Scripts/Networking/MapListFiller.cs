using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapListFiller : MonoBehaviour
{
    private TMP_Dropdown mapDropdown;

    void Start()
    {
        mapDropdown = GetComponent<TMP_Dropdown>();
        List<string> strings = new List<string>();
        foreach (string map in MapList.maps)
        {
            strings.Add(map);
        }
        mapDropdown.AddOptions(strings);
    }
}
