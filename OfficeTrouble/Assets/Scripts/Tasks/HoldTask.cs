using UnityEngine;

public class HoldTask : GenericTask
{
    
    [SerializeField] private float holdDuration;

    private bool _keyPressed;
    private float _timeSinceHold;
    
    protected override void SpecificUpdate()
    {
        
        if (_keyPressed)
            _timeSinceHold += Time.deltaTime;
        else
            _timeSinceHold = 0.0f;

        if (_timeSinceHold >= holdDuration)
        {
            TaskFulfilled = true;
        }
        
    }

    protected override float CalculateTaskProgress()
    {
        return _timeSinceHold / holdDuration;
    }

    protected override void OnKeyPressed()
    {
        _keyPressed = true;
        TaskIsBeingDealtWith = true;
    }

    protected override void OnKeyUnpressed()
    {
        _keyPressed = false;
        TaskIsBeingDealtWith = false;
    }
    
}
