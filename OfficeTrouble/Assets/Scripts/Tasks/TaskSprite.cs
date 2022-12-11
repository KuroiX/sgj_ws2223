using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskSprite : MonoBehaviour
{
    [SerializeField] private List<string> activates;
    [SerializeField] private List<string> deactivates;
    
    private Image f;
    
    private void Awake()
    {
        f = GetComponent<Image>();
    }

    public void Activate(bool value)
    {
        foreach (var goName in activates)
        {
			if (GameObject.Find(goName)) {
				var image = GameObject.Find(goName).GetComponent<Image>();
				image.enabled = value;
			}
        }

        foreach (var goName in deactivates)
        {
			if (GameObject.Find(goName))
			{
				var image = GameObject.Find(goName).GetComponent<Image>();
				image.enabled = !value;
			}
		}
    }
}
