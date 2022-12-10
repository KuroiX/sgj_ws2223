using System;
using UnityEngine;

public abstract class GenericTask : MonoBehaviour, ITask, IValueChanged
{
    // GameLogic
    
    [SerializeField] protected string keyName;
    [SerializeField] protected float initalDelaySeconds;
    [SerializeField] protected float stressIncrementPerTick;

    protected bool TaskIsBeingDealtWith;
    protected bool TaskFulfilled;
    
    private StressMeter _stressMeter;
    private bool _initialDelayOver;
    private float _passedSecondsSinceStart;
    private bool _lastKeyState;
    protected float _taskProgress;
    protected KeyWrapper _currentKey;

    #region Abstract Methods

    protected abstract void SpecificUpdate();

    protected abstract float CalculateTaskProgress();

    protected abstract void OnKeyPressed();

    protected abstract void OnKeyUnpressed();

    #endregion

    private void Start()
    {
        _stressMeter = GameObject.Find("GameController").GetComponent<GameController>().StressMeter;
    }

    public void Update()
    {
        HandleKeyState();

        if (!_initialDelayOver)
        {
            _passedSecondsSinceStart += Time.deltaTime;
            if (_passedSecondsSinceStart > initalDelaySeconds)
                _initialDelayOver = true;
        }
        
        SpecificUpdate();
        
        TaskProgress = CalculateTaskProgress();
    }
    
    private void FixedUpdate()
    {
        if (_initialDelayOver && !TaskIsBeingDealtWith)
            _stressMeter.IncreaseStressLevel(stressIncrementPerTick);
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

    public bool GetTaskFulfilled()
    {
        return TaskFulfilled;
    }

    public bool GetTaskIsBeingDealtWith()
    {
        return TaskIsBeingDealtWith;
    }

    public string GetKeyValue()
    {
        return _currentKey.GetKeyCode();
    }

    public event Action<float> ValueChanged;
    
    private float TaskProgress
    {
        get => _taskProgress;
        set
        {
            ValueChanged?.Invoke(value);
            _taskProgress = value;
        }
    }

}
