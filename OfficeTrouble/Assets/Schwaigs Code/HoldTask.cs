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
        Debug.Log("Task: Penalty applied! Penalty value: "+ PenaltyValue);
    }

    public override bool CheckTaskFulfilled()
    {
        return _taskFulfilled;
    }

    public override void SpecificUpdate()
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
        _keyState = true;
    }

    public override void OnKeyUnpressed()
    {
        _keyState = false;
        _timeSinceHold = 0.0f;
    }
}
