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
    private int _xCoordinate;
    private int _yCoordinate;

    // Corresponding key
    //private Key _key;
    
    // GameLogic
    [SerializeField] private float TickInterval = 0.5f;
    [SerializeField] private float TimeBeforeFirstTick = 1.0f;
    [SerializeField] protected int PenaltyValue = 10;
    [SerializeField] protected String ButtonValue;
    private float _timePassed;
    private bool _taskActive = false;
    protected bool _taskFulfilled;
    private bool _lastKeyState;

    // Public Methods
    public abstract void OnUncompleted();

    public abstract bool CheckTaskFulfilled();

    protected abstract void SpecificUpdate();

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
        // Penalty logic
        _timePassed += Time.deltaTime;
        if (_timePassed >= TickInterval)
        {
            _timePassed -= TickInterval;
            OnUncompleted();
        }
    }

    public void StartTask()
    {
        SpecificReset();
        _taskFulfilled = false;
        _taskActive = true;
        _timePassed = -TimeBeforeFirstTick;
    }

    public abstract void OnKeyPressed();

    public abstract void OnKeyUnpressed();

    protected abstract void SpecificReset();
    
    // Intern Methods
    protected void ResetTimer()
    {
        _timePassed = 0.0f;
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
