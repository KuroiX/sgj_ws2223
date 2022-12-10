using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldTask : GenericTask
{
    // Additional Parameter
    [SerializeField] private float HoldDuration = 6.0f;

    private float _timeSinceHold = 0.0f;
    private bool _taskFulfilled = false;
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

    public override void OnKeyPressed(object sender, EventArgs args)
    {
        Debug.Log("Key pressed " + ButtonValue);
        _keyState = true;
    }

    public override void OnKeyUnpressed(object sender, EventArgs args)
    {
        _keyState = false;
        _timeSinceHold = 0.0f;
    }
}
