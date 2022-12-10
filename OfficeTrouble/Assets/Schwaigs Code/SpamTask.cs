using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamTask : GenericTask
{
    // Additional Parameters
    [SerializeField] private int SpamNumber = 10;

    private int _numberPressed = 0;
    private bool _stateLastUpdate = false;

    // Update is called once per frame
    public override void OnUncompleted()
    {
        // ToDo: Implement penalty logic
        Debug.Log("Task: Penalty applied! Penalty value: "+ PenaltyValue);
    }

    public override bool CheckTaskFulfilled()
    {
        return _numberPressed >= SpamNumber;
    }

    protected override void SpecificUpdate()
    {
        // Nothing to be done here
    }

    public override void OnKeyPressed()
    {
        Debug.Log("Key "+ButtonValue+" pressed " + _numberPressed +" times");
        _numberPressed++;
    }

    public override void OnKeyUnpressed()
    {
        // Nothing to be done here
    }

    protected override void SpecificReset()
    {
        _numberPressed = 0;
        _stateLastUpdate = false;
    }
}
