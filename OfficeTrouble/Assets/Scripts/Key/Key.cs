using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Key : MonoBehaviour
{
    [SerializeField]
    private GameObject visual;
    private KeyControl identifier;
    private bool pressed;

    public Key (KeyControl identifier)
    {
        this.identifier = identifier;
    }

    private void CheckPressed()
    {
        
    }


    public bool GetPressed()
    {
        return pressed;
    }

}
