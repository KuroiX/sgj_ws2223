using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Key
{
    [SerializeField]
    private GameObject visual;
    private KeyControl identifier;
    private bool pressed;

    public Key(KeyControl identifier)
    {
        this.identifier = identifier;
    }

    public void ProcessKey()
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

    public bool GetPressed()
    {
        return pressed;
    }

}
