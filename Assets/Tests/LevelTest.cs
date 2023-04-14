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
    public IEnumerator MainMenuScene()
    {
        //Setup test
        SceneManager.LoadScene("0- Main Menu");
        yield return null; //Pass one frame

        //Check for missing prefabs
        MissingPrefabDetector.CheckForMissingPrefabsInScene();
    }

    [UnityTest]
    public IEnumerator MapScene()
    {
        //Setup test
        SceneManager.LoadScene("1- Map");
        yield return null; //Pass one frame

        //Check for missing prefabs
        MissingPrefabDetector.CheckForMissingPrefabsInScene();
        MissingPrefabDetector.CheckForMissingPrefabsInProject();
    }

    [UnityTest]
    public IEnumerator CombatScene()
    {
        //Setup test
        SceneManager.LoadScene("2- Combat Scene");
        yield return null; //Pass one frame

        //Check for missing prefabs
        MissingPrefabDetector.CheckForMissingPrefabsInScene();
    }

    [Test]
    public void CheckScriptableObjectsForMissingReferences() {
        // Get all scriptable objects in the Assets folder
        string[] guids = AssetDatabase.FindAssets("t:ScriptableObject");
        foreach (string guid in guids) {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            ScriptableObject scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
            // Get all serialized fields of the scriptable object
            var fields = scriptableObject.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields) {
                //Only check for references to specific types
                if (field.FieldType == typeof(Component) || 
                field.FieldType == typeof(Texture)  || 
                field.FieldType == typeof(string) ||
                field.FieldType == typeof(AnimationClip)){
                    // Get the value of the field
                    object fieldValue = field.GetValue(scriptableObject);
                    // Check if the value is null
                    
                    if(fieldValue == null)
                    {
                        Debug.LogError("Reference is missing in scriptable object " + scriptableObject.name +" of type: " + scriptableObject.GetType().Name + "." + " for variable " + field.Name + ".");
                    }
                }
            }
        }
    }
}
