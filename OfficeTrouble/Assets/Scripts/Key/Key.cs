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

    private void Update()
    {
        if (identifier.wasPressedThisFrame)
        {
            pressed = true;
        }

        if (identifier.wasReleasedThisFrame)
        {
            pressed = false;
        }
    }

    public Key (KeyControl identifier)
    {
        this.identifier = identifier;
    }


    public bool GetPressed()
    {
        return pressed;
    }

}
