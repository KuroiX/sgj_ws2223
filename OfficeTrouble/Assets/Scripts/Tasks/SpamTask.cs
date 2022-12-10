using UnityEngine;

public class SpamTask : GenericTask
{
    
    [SerializeField] private int numberToPress;
    [SerializeField] private float allowedSecondBetweenPresses;

    private int _numberPressed;
    private float _passedSecondsSinceLastPress;

    protected override void SpecificUpdate()
    {
        _passedSecondsSinceLastPress += Time.deltaTime;
        if (TaskIsBeingDealtWith && _passedSecondsSinceLastPress > allowedSecondBetweenPresses)
            TaskIsBeingDealtWith = false;
    }

    protected override float CalculateTaskProgress()
    {
        return _numberPressed / (float) numberToPress;
    }
    
    protected override void OnKeyPressed()
    {
        _numberPressed++;
        _passedSecondsSinceLastPress = 0f;
        
        TaskIsBeingDealtWith = true;
        
        if (_numberPressed > numberToPress)
            TaskFulfilled = true;
    }

    protected override void OnKeyUnpressed()
    {
        // nothing to do here
    }

}
