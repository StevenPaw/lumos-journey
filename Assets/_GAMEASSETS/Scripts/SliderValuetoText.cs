using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderValuetoText : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private Text textSliderValue;

    private void Start()
    {
        textSliderValue = GetComponent<Text>();
        ShowSliderValue();

    }

    public void ShowSliderValue ()
    {
        string sliderMessage = "Volume =" + slider.value;
        textSliderValue.text = sliderMessage;
    }
}
