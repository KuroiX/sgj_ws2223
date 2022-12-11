using System;

public class StressMeter: IValueChanged
{

    private const float MaxValue = 100f;
    
    public event Action<float> ValueChanged;

	private void OnValueChanged(float value)
    {
        ValueChanged?.Invoke(value);
    }

    private float _stressLevel;

    private float StressLevel
    {
        get => _stressLevel;
        set
        {
            OnValueChanged(value / MaxValue);
            _stressLevel = value;
        }
    }

    public void IncreaseStressLevel(float value)
    {
        StressLevel += value;
    }

    public void DecreaseStressLevel(float value)
    {
        StressLevel -= value;
    }

	public bool IsGameLost()
	{
		return _stressLevel >= MaxValue;
	}

}
