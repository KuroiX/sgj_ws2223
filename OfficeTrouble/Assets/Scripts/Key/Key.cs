using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Key
{
    [SerializeField]
    private GameObject visual;
    private string identifier;
    public event EventHandler keyPressed;
    public event EventHandler keyReleased;

    public Key(string identifier)
    {
        this.identifier = identifier;
    }

    public void ProcessKey()
    {
        if (((KeyControl)Keyboard.current[identifier]).wasPressedThisFrame)
        {
            OnKeyPressed();
        }

        if (((KeyControl)Keyboard.current[identifier]).wasReleasedThisFrame)
        {
            OnKeyReleased();
        }
    }

    private void OnKeyPressed()
    {
        keyPressed?.Invoke(this, EventArgs.Empty);
    }

    private void OnKeyReleased()
    {
        keyReleased?.Invoke(this, EventArgs.Empty);
    }

}
