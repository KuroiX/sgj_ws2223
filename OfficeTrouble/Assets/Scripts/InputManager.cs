using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputManager : MonoBehaviour
{

	public static InputManager Instance;

	private Dictionary<string, bool> _uiKeyPressedDict;

	private void Awake()
	{
		Instance = this;
		_uiKeyPressedDict = new Dictionary<string, bool>();
	}

	public bool KeyIsPressed(string key)
	{
		if (key == "Mouse0")
		{
			return Input.GetKey(KeyCode.Mouse0);
		}
		
		bool uiKeyPressed = _uiKeyPressedDict.ContainsKey(key) && _uiKeyPressedDict[key];
		return uiKeyPressed || ((KeyControl) Keyboard.current[key]).isPressed;
	}


	public void UIKeyPressed(string key)
	{
		if (_uiKeyPressedDict.ContainsKey(key))
		{
			_uiKeyPressedDict[key] = true;
		}
		else
		{
			_uiKeyPressedDict.Add(key, true);
		}
	}

	public void UIKeyReleased(string key)
	{
		if (_uiKeyPressedDict.ContainsKey(key))
		{
			_uiKeyPressedDict[key] = false;
		}
		else
		{
			// should never happen!
			_uiKeyPressedDict.Add(key, true);
		}
	}

}
