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

    public override void OnKeyPressed(object sender, EventArgs args)
    {
        Debug.Log("Key pressed " + ButtonValue);
        _numberPressed++;
    }

    public override void OnKeyUnpressed(object sender, EventArgs args)
    {
        // Nothing to be done here
    }
}
