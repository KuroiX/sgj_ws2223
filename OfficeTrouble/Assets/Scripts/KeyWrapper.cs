using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyKeyWrapper", menuName = "ScriptableObject/KeyWrapper", order = 99)]
public class KeyWrapper : ScriptableObject
{
    [SerializeField] private string KeyCode;

    [SerializeField] private string UIText;


    public string GetKeyCode()
    {
        return KeyCode;
    }

    public string GetUIText()
    {
        return UIText;
    }
}
