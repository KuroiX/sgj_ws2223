using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class GenericTask : ScriptableObject, ITask
{
    // Private Variables
    // Screen Coordinates
    private int _xCoordinate;
    private int _yCoordinate;

    // Corresponding key
    private Key _key;
    
    // GameLogic
    [SerializeField] private float TickInterval = 0.5f;
    [SerializeField] private float TimeBeforeFirstTick = 1.0f;
    [SerializeField] protected int PenaltyValue = 10;
    [SerializeField] protected String ButtonValue;
    private float _timePassed;
    private bool _taskActive = false;
    private bool _taskFulfilled;

    // Public Methods
    public abstract void OnUncompleted();

    public abstract bool CheckTaskFulfilled();

    protected abstract void SpecificUpdate();

    public void ProcessUpdate()
    {
        // Check if Task is active
        if (!_taskActive) return;
        
        _key.ProcessKey();
        
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
        _taskActive = true;
        _timePassed = -TimeBeforeFirstTick;
        
        _key = new Key(ButtonValue);
        _key.keyPressed += OnKeyPressed;
        _key.keyReleased += OnKeyUnpressed;
    }

    public abstract void OnKeyPressed(object sender, EventArgs args);

    public abstract void OnKeyUnpressed(object sender, EventArgs args);
    
    // Intern Methods
    protected void ResetTimer()
    {
        _timePassed = 0.0f;
    }

    protected void EndTask()
    {
        _taskActive = false;
        _taskFulfilled = false;

        _key.keyPressed -= OnKeyPressed;
        _key.keyReleased -= OnKeyUnpressed;
    }
}
