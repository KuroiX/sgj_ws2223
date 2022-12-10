using System;
using UnityEngine;

public abstract class GenericTask : MonoBehaviour, ITask, IValueChanged
{
    // GameLogic
    //[SerializeField] protected string keyName;
    [SerializeField] protected float timeBeforeFirstTick = 1.0f;
    [SerializeField] protected float tickInterval = 0.5f;
    [SerializeField] protected int penaltyValue = 10;
    protected bool TaskFulfilled;
    private bool _lastKeyState;
    protected float _taskProgress;
    protected KeyWrapper _currentKey;

    #region Abstract Methods
    
    protected abstract void SpecificUpdate();

    protected abstract void SpecificAwake();
    
    public abstract bool CheckTaskFulfilled();
    
    public abstract void OnPenalty();
    
    public abstract void OnKeyPressed();

    public abstract void OnKeyUnpressed();

    protected abstract float CalculateTaskProgress();
    
    #endregion

    public void Awake()
    {
        SpecificAwake();
    }
    public void Update()
    {
        // Check the key state
        HandleKeyState();
        
        // Execute Task-specific logic
        SpecificUpdate();
        
        // Calculate Task progress
        float oldProgress = _taskProgress;
        _taskProgress = CalculateTaskProgress();
        if (_taskProgress != oldProgress)
        {
            taskProgress = _taskProgress;
        }
        //ValueChanged?.Invoke(_taskProgress);
        
        // Check if Task is fulfilled
        TaskFulfilled = CheckTaskFulfilled();
        if (TaskFulfilled)
        {
            Destroy(gameObject);
        }
    }
    
    private void HandleKeyState()
    {
        bool newKeyState = InputManager.Instance.KeyIsPressed(_currentKey.GetKeyCode());
        if (newKeyState && !_lastKeyState)
        {
            OnKeyPressed();
        }

        if (!newKeyState && _lastKeyState)
        {
            OnKeyUnpressed();
        }

        _lastKeyState = newKeyState;
    }

    public string GetKeyName()
    {
        return _currentKey.GetUIText();
    }

    public string GetKeyValue()
    {
        return _currentKey.GetKeyCode();
    }

    public event Action<float> ValueChanged;
    
    private float taskProgress
    {
        get => _taskProgress;
        set
        {
            
            ValueChanged?.Invoke(value);
            _taskProgress = value;
        }
    }
}
