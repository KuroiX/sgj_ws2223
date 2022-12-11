using System;
using System.Collections;
using UnityEngine;

public abstract class GenericTask : MonoBehaviour, ITask, IValueChanged
{
    // GameLogic
    
    [SerializeField] protected KeyWrapper key;
    [SerializeField] protected float initalDelaySeconds;
    [SerializeField] protected float stressIncrementPerTick;
    [SerializeField] private AudioPlayScript.SoundClip soundClip;
    [SerializeField] private string spriteName;
    [SerializeField] public CatTaskInfo catTaskInfo;
    private TaskSprite _taskSprite;
    
    
    [SerializeField] private bool RandomLoopSounds = false; 

    protected bool TaskIsBeingDealtWith;
    protected bool TaskFulfilled;
    
    private StressMeter _stressMeter;
    private bool _initialDelayOver;
    private float _passedSecondsSinceStart;
    private bool _lastKeyState;
    private float _taskProgress;
    private float _elapsedTimeSinceStart;

    #region Abstract Methods

    protected abstract void SpecificUpdate();

    protected abstract float CalculateTaskProgress();

    protected abstract void OnKeyPressed();

    protected abstract void OnKeyUnpressed();

    #endregion

    protected void Start()
    {
        _stressMeter = GameObject.Find("GameController").GetComponent<GameController>().StressMeter;
        PlayTaskSound();
        _taskSprite = GetComponent<TaskSprite>();
        _taskSprite.Activate(true);
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

    private void OnDestroy()
    {
        if (_taskSprite) _taskSprite.Activate(false);
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

    private void PlayTaskSound()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = AudioManagerScript.Instance.SoundEffectMixer;
        source.clip = AudioPlayScript.GetAudioClip(soundClip);
        source.Play();
    }

}
