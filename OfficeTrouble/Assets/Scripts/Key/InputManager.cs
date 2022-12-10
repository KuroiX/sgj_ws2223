using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputManager: MonoBehaviour
{
    public static InputManager Instance;

    private void Awake()
    {
        Instance = this;
        uiKeyPressedDict = new Dictionary<string, bool>();
    }

    [SerializeField]
    private GameObject visual;

    private Dictionary<string, bool> uiKeyPressedDict;


    public void UIKeyPressed(string key)
    {
        if (uiKeyPressedDict.ContainsKey(key))
        {
            uiKeyPressedDict[key] = true;
        }
        else
        {
            uiKeyPressedDict.Add(key, true);
        }
    }

    public void UIKeyReleased(string key)
    {
        if (uiKeyPressedDict.ContainsKey(key))
        {
            uiKeyPressedDict[key] = false;
        }
        else
        {
            // should never happen!
            uiKeyPressedDict.Add(key, true);
        }
    }

    public bool KeyIsPressed(string key)
    {
        bool uiKeyPressed = uiKeyPressedDict.ContainsKey(key) && uiKeyPressedDict[key];
        return uiKeyPressed || ((KeyControl)Keyboard.current[key]).wasReleasedThisFrame;
    }

}
