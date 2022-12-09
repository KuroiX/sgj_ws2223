using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
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
        Debug.Log("Task: Penalty applied! Penalty value: "+ PenaltyValue);
    }

    public override bool CheckTaskFulfilled()
    {
        return _numberPressed >= SpamNumber;
    }

    public override void SpecificUpdate()
    {
        // Nothing to be done here
    }

    public override void OnKeyPressed()
    {
        _numberPressed++;
    }

    public override void OnKeyUnpressed()
    {
        // Nothing to be done here
    }
}
