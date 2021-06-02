using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewSavePanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Image inputFieldImage;
    [SerializeField] private TMP_Text inputFieldPlaceholderText;
    [SerializeField] private SaveSystemManager saveSystemManager;

    private void Start()
    {
        saveSystemManager = GameObject.Find("SaveSystemManager").GetComponent<SaveSystemManager>();
    }

    public void OnNewButtonPress()
    {
        if (inputField.text == "")
        {
            inputFieldImage.color = Color.red;
            inputFieldPlaceholderText.text = "ENTER NAME PLEASE";
        }
        else
        {
            if (saveSystemManager.DoesSaveNameExist(inputField.text))
            {
                inputFieldImage.color = Color.yellow;
            }
            else
            {
                saveSystemManager.SaveFile(inputField.text);
                saveSystemManager.ShowSaves();
            }
        }
    }

    public void OnInputFieldChange()
    {
        if (inputField.text == "")
        {
            inputFieldImage.color = Color.red;
            inputFieldPlaceholderText.text = "ENTER NAME PLEASE";
        }
        else
        {
            inputFieldImage.color = Color.white;
            inputFieldPlaceholderText.text = "Enter name...";
        }
    }
}
