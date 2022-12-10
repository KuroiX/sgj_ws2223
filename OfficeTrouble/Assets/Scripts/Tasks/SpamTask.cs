using UnityEngine;

public class SpamTask : GenericTask
{
    // Additional Parameters
    [SerializeField] private int spamNumber = 10;

    private int _numberPressed;
    private float _timeSincePenalty;
    
    protected void Start()
    {
        _timeSincePenalty = -timeBeforeFirstTick;
    }
    
    protected override void SpecificUpdate()
    {
        _timeSincePenalty += Time.deltaTime;
        /*// Penalty logic
        if (_timeSincePenalty >= 0)
        {
            OnPenalty();
            _timeSincePenalty =  _timeSincePenalty - tickInterval;
        }*/
    }

    public override bool CheckTaskFulfilled()
    {
        return _numberPressed >= spamNumber;
    }
    
    public override void OnPenalty()
    {
        // ToDo: Implement penalty logic
        Debug.Log("Task: Penalty applied! Penalty value: "+ penaltyValue);
    }

    public override void OnKeyPressed()
    {
        _numberPressed++;
        if (_numberPressed > spamNumber)
        {
            TaskFulfilled = true;
        }
        Debug.Log("Key " + keyName + " pressed " + _numberPressed + " times");
        
    }

    public override void OnKeyUnpressed()
    {
        // Nothing to be done here
    }

    protected override float CalculateTaskProgress()
    {
        return _numberPressed / spamNumber;
    }
}
