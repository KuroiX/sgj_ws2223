using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldTask : GenericTask
{
    // Additional Parameter
    [SerializeField] private float HoldDuration = 6.0f;

    private float _timeSinceHold = 0.0f;
    private bool _keyState = false;
    
    public override void OnUncompleted()
    {
        // ToDo: Implement penalty logic
        Debug.Log("Task: Penalty applied! Penalty value: "+ PenaltyValue);
    }

    public override bool CheckTaskFulfilled()
    {
        return _taskFulfilled;
    }

    protected override void SpecificUpdate()
    {
        if (_keyState)
        {
            _timeSinceHold += Time.deltaTime;
        }
        if (_timeSinceHold >= HoldDuration)
        {
            _taskFulfilled = true;
        }
    }

    public override void OnKeyPressed()
    {
        Debug.Log("Key "+ButtonValue+"pressed.");
        _keyState = true;
    }

    public override void OnKeyUnpressed()
    {
        Debug.Log("Key "+ButtonValue+" released.");
        _keyState = false;
        _timeSinceHold = 0.0f;
    }
    protected override void SpecificReset()
    {
        _timeSinceHold = 0.0f;
        _taskFulfilled = false;
        _keyState = false;
    }
}
