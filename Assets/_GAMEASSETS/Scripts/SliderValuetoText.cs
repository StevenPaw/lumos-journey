using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SliderValuetoText : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private TextMeshProUGUI textMesh;

    public void ShowSliderValue ()
    {
        string sliderMessage = "Volume =" + (int)(slider.value * 100);
        textMesh.text =  sliderMessage;
    }
}
