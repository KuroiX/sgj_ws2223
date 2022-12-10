using System;

public interface IValueChanged
{
    event Action<float> ValueChanged;
}