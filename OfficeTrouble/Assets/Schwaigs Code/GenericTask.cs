using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericTask : MonoBehaviour, ITask
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
    private float _timePassed;
    private bool _taskActive = false;
    private bool _taskFulfilled;

    public abstract void OnUncompleted();

    public abstract bool CheckTaskFulfilled();

    public abstract void SpecificUpdate();

    public void Update()
    {
        // Check if Task is active
        if (!_taskActive) return;
        
        // Execute Task-specific logic
        SpecificUpdate();
        
        // Check if Task is fulfilled
        _taskFulfilled = CheckTaskFulfilled();
        
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
    }

    protected void ResetTimer()
    {
        _timePassed = 0.0f;
    }

    public abstract void OnKeyPressed();

    public abstract void OnKeyUnpressed();
}
