using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    
    private Slider _slider;

    public float sliderValue;

    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    public void SetSliderValue(float value)
    {
        _slider.value = value;
        sliderValue = value;
    }

    public void IncrementSlider(float value)
    {
        _slider.value += value;
        sliderValue += value;
    }
}
