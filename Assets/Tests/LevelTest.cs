using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.Reflection;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;


public class LevelTest
{
    [UnityTest]
    public IEnumerator MainScene()
    {
        //Setup test
        SceneManager.LoadScene("MainScene");
        yield return null; //Pass one frame

        //Check for missing prefabs
        MissingPrefabDetector.CheckForMissingPrefabsInScene();
    }

}
