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
    private float _timeSincePenalty;

    // Update is called once per frame
    public override void OnPenalty()
    {
        // ToDo: Implement penalty logic
        Debug.Log("Task: Penalty applied! Penalty value: "+ PenaltyValue);
    }

    public override bool CheckTaskFulfilled()
    {
        return _numberPressed >= SpamNumber;
    }

    protected override void SpecificStart()
    {
        _timeSincePenalty = -TimeBeforeFirstTick;
    }
    protected override void SpecificUpdate()
    {
        _timeSincePenalty += Time.deltaTime;
        // Penalty logic
        if (_timeSincePenalty >= 0)
        {
            OnPenalty();
            _timeSincePenalty =  _timeSincePenalty - TickInterval;
        }
    }

    public override void OnKeyPressed()
    {
        _numberPressed++;
        Debug.Log("Key "+ButtonValue+" pressed " + _numberPressed +" times");
        
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
