using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class GenericTask : ScriptableObject, ITask
{
    // Private Variables
    // Screen Coordinates
    [SerializeField]private int _xCoordinate;
    [SerializeField]private int _yCoordinate;

    // Corresponding key
    //private Key _key;
    
    // GameLogic
    [SerializeField] protected float TickInterval = 0.5f;
    [SerializeField] protected float TimeBeforeFirstTick = 1.0f;
    [SerializeField] protected int PenaltyValue = 10;
    [SerializeField] protected String ButtonValue;
    private bool _taskActive = false;
    protected bool _taskFulfilled;
    private bool _lastKeyState = false;

    #region Abstract Methods

    public abstract void OnPenalty();

    public abstract bool CheckTaskFulfilled();

    protected abstract void SpecificUpdate();
    
    public abstract void OnKeyPressed();

    public abstract void OnKeyUnpressed();

    protected abstract void SpecificReset();

    protected abstract void SpecificStart();

    #endregion
    

    public void ProcessUpdate()
    {
        // Check if Task is active
        if (!_taskActive) return;
        
        // Check the key state
        CheckKeyState();
        
        // Execute Task-specific logic
        SpecificUpdate();
        
        // Check if Task is fulfilled
        _taskFulfilled = CheckTaskFulfilled();
        if (_taskFulfilled)
        {
            EndTask();
            return;
        }
    }

    public void StartTask()
    {
        SpecificReset();
        SpecificStart();
        _taskFulfilled = false;
        _taskActive = true;
    }

    protected void EndTask()
    {
        _taskActive = false;
        _taskFulfilled = false;
    }

    private void CheckKeyState()
    {
        bool newKeyState = InputManager.Instance.KeyIsPressed(ButtonValue);
        if (newKeyState && !_lastKeyState)
        {
            OnKeyPressed();
        }

        if (!newKeyState && _lastKeyState)
        {
            OnKeyUnpressed();
        }

        _lastKeyState = newKeyState;
    }

    public String GetKeyValue()
    {
        return ButtonValue;
    }

    public int GetXCoord()
    {
        return _xCoordinate;
    }

    public int GetYCoord()
    {
        return _yCoordinate;
    }
}
