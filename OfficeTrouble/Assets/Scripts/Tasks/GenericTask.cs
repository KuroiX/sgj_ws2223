using UnityEngine;

public abstract class GenericTask : MonoBehaviour, ITask
{
    // GameLogic
    [SerializeField] protected string keyName;
    [SerializeField] protected float timeBeforeFirstTick = 1.0f;
    [SerializeField] protected float tickInterval = 0.5f;
    [SerializeField] protected int penaltyValue = 10;
    protected bool TaskFulfilled;
    private bool _lastKeyState;

    #region Abstract Methods
    
    protected abstract void SpecificUpdate();

    public abstract bool CheckTaskFulfilled();
    
    public abstract void OnPenalty();
    
    public abstract void OnKeyPressed();

    public abstract void OnKeyUnpressed();
    
    #endregion
    
    public void Update()
    {
        // Check the key state
        HandleKeyState();
        
        // Execute Task-specific logic
        SpecificUpdate();
        
        // Check if Task is fulfilled
        TaskFulfilled = CheckTaskFulfilled();
        if (TaskFulfilled)
        {
            Destroy(gameObject);
        }
    }
    
    private void HandleKeyState()
    {
        bool newKeyState = InputManager.Instance.KeyIsPressed(keyName);
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
        return keyName;
    }

}