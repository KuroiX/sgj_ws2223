using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SequentialTask : MonoBehaviour//GenericTask
{
    /*
    // Additional Parameters
    //[SerializeField] protected string keySequence;

    [SerializeField]
    protected List<KeyWrapper> KeyWrappers;
    
    // Internal parameters
    private KeyWrapper[] _keys;
    private int _sequenceLength;
    private int _currentPosition = 0;
    private bool _keyIsPressed = false;
    private bool _taskFullfilled;
    private float _timeSincePenalty;
    
    protected override void SpecificUpdate()
    {
        _timeSincePenalty += Time.deltaTime;
        // Penalty logic
        if (_timeSincePenalty >= 0)
        {
            OnPenalty();
            ResetSequence();
            _timeSincePenalty =  _timeSincePenalty - tickInterval;
        }
    }

    protected override void SpecificAwake()
    {
        _sequenceLength = KeyWrappers.Count;
        _keys = new KeyWrapper[_sequenceLength];
        for (int i = 0; i < _sequenceLength; i++)
        {
            _keys[i] = KeyWrappers[i];
        }

        _currentKey = _keys[_currentPosition];
    }

    public override bool CheckTaskFulfilled()
    {
        return _taskFullfilled;
    }

    public override void OnPenalty()
    {
        // ToDo: Implement penalty logic
        Debug.Log("Task: Penalty applied! Penalty value: "+ penaltyValue);
    }

    public override void OnKeyPressed()
    {
        _keyIsPressed = true;
    }

    public override void OnKeyUnpressed()
    {
        _keyIsPressed = false;
        _currentPosition++;
        if (_currentPosition >= _sequenceLength)
        {
            _taskFullfilled = true;
        }
        else
        {
            _currentKey = _keys[_currentPosition];
            _timeSincePenalty = -tickInterval;
            GetComponentInChildren<TextMeshProUGUI>().text = _currentKey.GetUIText().ToUpper();
        }
        
    }

    protected override float CalculateTaskProgress()
    {
        return (float) _currentPosition / _sequenceLength;
    }

    private void ResetSequence()
    {
        _currentPosition = 0;
        _currentKey = _keys[_currentPosition];
        GetComponentInChildren<TextMeshProUGUI>().text = _currentKey.GetUIText().ToUpper();
    }
    */
}
