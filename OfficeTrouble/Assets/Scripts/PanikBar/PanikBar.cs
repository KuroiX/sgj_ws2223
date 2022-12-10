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
    private float fillValue = 0f;

    [SerializeField]
    private Slider slider;

    private void Update()
    {
        fillValue += 0.0001f;
        ChangeFill(fillValue);
    }

    public void ChangeFill(float amount)
    {
        if(amount >= 1)
        {
            // Player has lost
        }

        slider.value = fillValue;

        if (fillValue > 0.7)
        {
            sliderImage.color = Color.Lerp(Color.red, Color.white, Mathf.PingPong(Time.time, 1f));
        }
        else
        {
            sliderImage.color = Color.Lerp(Color.green, Color.red, fillValue / 1);
        }
    }

    public void Register()
    {

    }

}
