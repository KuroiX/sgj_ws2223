using UnityEngine;

public class SpamTask : GenericTask
{
    // Additional Parameters
    //[SerializeField] protected string keyToPress;
    //[SerializeField] protected string UIText;
    [SerializeField] protected KeyWrapper KeyWrapper;
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
        // Penalty logic
        if (_timeSincePenalty >= 0)
        {
            OnPenalty();
            _timeSincePenalty =  _timeSincePenalty - tickInterval;
        }
    }

    protected override void SpecificAwake()
    {
        _currentKey = KeyWrapper.GetKeyCode();
        _UIKey = KeyWrapper.GetUIText();
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
        Debug.Log("Key " + _currentKey + " pressed " + _numberPressed + " times");
        
    }

    public override void OnKeyUnpressed()
    {
        // Nothing to be done here
    }

    protected override float CalculateTaskProgress()
    {
        return (float) _numberPressed / spamNumber;
    }
}
