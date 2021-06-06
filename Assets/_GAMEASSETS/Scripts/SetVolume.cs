using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; //required to acces Audio Mixer functions
public class SetVolume : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer; 

    public void SetLevel (float sliderValue) //needs to be public to be exposed in the "on value chagned" event of the VolumeSlider
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
}
