using TMPro;
using UnityEngine;

public class BuildDebugger : MonoBehaviour
{

	private static BuildDebugger _instance;
	private TextMeshProUGUI _debugPrompt;

	private void Awake()
	{
		_instance = this;
		_instance._debugPrompt = GameObject.Find("DebugPrompt").GetComponent<TextMeshProUGUI>();
	}

	private void Start()
	{
	}

	public static void SetText(string text)
	{
		_instance._debugPrompt.text = text;
	}

	public static string GetText()
	{
		return _instance._debugPrompt.text;
	}
	
}
