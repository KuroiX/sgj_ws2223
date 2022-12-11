using System;
using UnityEngine;

public abstract class GenericTask : MonoBehaviour, ITask, IValueChanged
{
    // GameLogic
    
    [SerializeField] protected KeyWrapper key;
    [SerializeField] protected float initalDelaySeconds;
    [SerializeField] protected float stressIncrementPerTick;

    protected bool TaskIsBeingDealtWith;
    protected bool TaskFulfilled;
    
    private StressMeter _stressMeter;
    private bool _initialDelayOver;
    private float _passedSecondsSinceStart;
    private float _taskProgress;
    private bool _lastKeyState;

    #region Abstract Methods

    protected abstract void SpecificUpdate();

    protected abstract float CalculateTaskProgress();

    protected abstract void OnKeyPressed();

    protected abstract void OnKeyUnpressed();

    #endregion

    protected void Start()
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
			{
				_initialDelayOver = true;
			}
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
        bool newKeyState = InputManager.Instance.KeyIsPressed(key.GetKeyCode());
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

    public bool GetTaskFulfilled()
    {
        return TaskFulfilled;
    }

    public bool GetTaskIsBeingDealtWith()
    {
        return TaskIsBeingDealtWith;
    }

	public string GetKeyName()
	{
		return key.GetUIText();
	}

    public string GetKeyValue()
    {
        return key.GetKeyCode();
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
