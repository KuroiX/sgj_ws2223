using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputManager: MonoBehaviour
{
    public static InputManager Instance;

    [SerializeField]
    private GameObject visual;

    private Dictionary<string, bool> uiKeyPressedDict;

    private void Awake()
    {
        Instance = this;
        uiKeyPressedDict = new Dictionary<string, bool>();
    }


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
        if (key == "")
        {
            Debug.Log("Warning: GameObject " + gameObject.name + " probably has a task script associated with no key name set!");
        }
        bool uiKeyPressed = uiKeyPressedDict.ContainsKey(key) && uiKeyPressedDict[key];
        return uiKeyPressed || ((KeyControl)Keyboard.current[key]).isPressed;
    }

}
