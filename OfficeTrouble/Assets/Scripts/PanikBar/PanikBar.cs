using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanikBar : MonoBehaviour
{
    [SerializeField]
    private Image sliderImage;
    [SerializeField]
    private RectTransform sliderRecttrans;
    [SerializeField]
    bool panikBar;
    private float fillValue = 0f;

    [SerializeField]
    private Slider slider;

    private IValueChanged valueChangedEvent;

    public void ChangeFill(float amount)
    {

        slider.value = fillValue;

        if (panikBar)
        {
            if (fillValue > 0.7)
            {
                sliderImage.color = Color.Lerp(Color.red, new Color(0.2f, 0, 0, 1), Mathf.PingPong(Time.time, 1f));
            }
            else
            {
                sliderImage.color = Color.Lerp(Color.green, Color.red, fillValue / 1);
            }
        }

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