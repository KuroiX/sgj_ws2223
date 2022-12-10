using System;
using System.Collections.Generic;
using UnityEngine;

public class StressMeter: IValueChanged
{
    public event Action<float> ValueChanged;

    private void OnValueChanged(float value)
    {
        ValueChanged?.Invoke(value);
    }

    private float _stressLevel;

    private float stressLevel
    {
        get => _stressLevel;
        set
        {
            OnValueChanged(value);
            _stressLevel = value;
        }
    }

    public void OnUpdate(List<GenericTask> activeTasks)
    {
        // TODO: proper formula

        float amount = activeTasks.Count;

        stressLevel += (float)amount / 50f * Time.deltaTime;
    }
}