using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateSpamTask {
    [MenuItem("Assets/Create/New SpamTask")]
    public static void CreateNewSpamTask()
    {
        SpamTask asset = ScriptableObject.CreateInstance<SpamTask>();

        AssetDatabase.CreateAsset(asset, "Assets/Tasks/NewSpamTask.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}