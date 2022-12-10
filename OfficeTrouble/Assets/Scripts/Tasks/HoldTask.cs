using UnityEngine;

public class HoldTask : GenericTask
{
    // Additional Parameter
    [SerializeField] private float holdDuration = 6.0f;

    private bool _keyState;
    private float _timeSinceHold;
    private float _timeSincePenalty;
    
    protected void Start()
    {
        _timeSincePenalty = -timeBeforeFirstTick;
    }
    
    protected override void SpecificUpdate()
    {
        if (_keyState)
        {
            _timeSinceHold += Time.deltaTime;
            //Debug.Log("increased..");
        }
        else
        {
            _timeSinceHold = 0.0f;
            _timeSincePenalty += Time.deltaTime;
            if (_timeSincePenalty >= 0)
            {
                OnPenalty();
                _timeSincePenalty -= tickInterval;
            }
        }

        if (_timeSinceHold >= holdDuration)
        {
            TaskFulfilled = true;
        }
    }

    public override bool CheckTaskFulfilled()
    {
        return TaskFulfilled;
    }
    
    public override void OnPenalty()
    {
        // ToDo: Implement penalty logic
        Debug.Log("Task: Penalty applied! Penalty value: "+ penaltyValue);
    }
    
    public override void OnKeyPressed()
    {
        Debug.Log("Key " + keyName + " pressed.");
        _keyState = true;
    }

    public override void OnKeyUnpressed()
    {
        Debug.Log("Key "+keyName+" released.");
        _keyState = false;
    }

    protected override float CalculateTaskProgress()
    {
        return _timeSinceHold / holdDuration;
    }
}
