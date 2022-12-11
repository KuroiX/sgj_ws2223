using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskBar : MonoBehaviour
{
    [SerializeField]
    private Image sliderImage;
    [SerializeField]
    private RectTransform sliderRecttrans;
    [SerializeField]
    private float fillValue = 0f;

    [SerializeField]
    private Slider slider;

    private GenericTask _task;
    private IValueChanged valueChangedEvent;

    public void ChangeFill(float amount)
    {
        fillValue = amount;
        slider.value = fillValue;
	}

    public void Awake()
    {
        _task = GetComponentInParent<GenericTask>();
        Register(_task);
    }

    public void Register(IValueChanged value)
    {
        valueChangedEvent = value;
        valueChangedEvent.ValueChanged += ChangeFill;
    }

    public void OnDestroy()
    {
        if(valueChangedEvent != null)
        {
            valueChangedEvent.ValueChanged -= ChangeFill;
        }

    }

}