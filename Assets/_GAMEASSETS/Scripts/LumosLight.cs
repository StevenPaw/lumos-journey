using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LumosLight : MonoBehaviour
{
    [SerializeField] private bool lightIsOn;
    [Header("Light Configuration")]
    [Range(0f,10f)] [SerializeField] private float lightIntensity;
    [SerializeField] private Light headLight;
    [SerializeField] private Color lightColor;
    
    [Header("Mesh and Materials")]
    [SerializeField] private Material offMaterial;
    [SerializeField] private Material onMaterial;
    [SerializeField] private GameObject lumosMesh;
    
    private SkinnedMeshRenderer meshRenderer;
    private Material[] mats;

    private void Start()
    {
        meshRenderer = lumosMesh.GetComponent<SkinnedMeshRenderer>(); //Get the renderer of the mesh for changing materials
        mats = meshRenderer.materials; //Get the Materials Array
    }

    void Update()
    {
        if (lightIsOn)
        {
            mats[2] = onMaterial; //Change the Material in the array for the head
            meshRenderer.materials = mats; //put the new material array into the renderer
            headLight.intensity = lightIntensity; //set the intensity of the light
            headLight.color = lightColor; //change the color of the light for dynamic light changing
        }
        else
        {
            mats[2] = offMaterial; //Change the Material in the array for the head
            meshRenderer.materials = mats; //put the new material array into the renderer
            headLight.intensity = 0f; //set the intensity of the light to 0 to turn it off
        }
    }
    
    #region Public Functions
    
    /// <summary>
    /// Change the intensity of the Headlight when its switched on
    /// </summary>
    /// <param name="intensityIn">(float) the new intensity to set the headlight to</param>
    public void ChangeIntensity(float intensityIn)
    {
        lightIntensity = intensityIn;
    }
    
    /// <summary>
    /// Change the intensity of the Headlight when its switched on
    /// </summary>
    /// <param name="intensityIn">(int) the new intensity to set the headlight to</param>
    public void ChangeIntensity(int intensityIn)
    {
        lightIntensity = intensityIn;
    }

    /// <summary>
    /// Change the color of the Headlight when its switched on
    /// </summary>
    /// <param name="colorIn">(color) the new lightcolor to set the headlight to</param>
    public void ChangeColor(Color colorIn)
    {
        lightColor = colorIn;
    }

    /// <summary>
    /// Switch the Headlight on or off
    /// </summary>
    public void Switch()
    {
        lightIsOn = !lightIsOn;
    }

    /// <summary>
    /// Switch the Headlight on or off to the given State
    /// </summary>
    /// /// <param name="isOn">(bool) set if the headlight should be switched on</param>
    public void Switch(bool isOn)
    {
        lightIsOn = isOn;
    }
    #endregion
}
