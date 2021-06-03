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
    [SerializeField] private GameObject newSaveButton;
    [SerializeField] private GameObject overrideSaveButton;
 
    private void Start()
    {
        saveSystemManager = GameObject.Find("SaveSystemManager").GetComponent<SaveSystemManager>();
        if (saveSystemManager.CurrentSaveName != "")
        {
            inputField.text = saveSystemManager.CurrentSaveName;
        }
    }

    public void OnNewButtonPress()
    {
        if (inputField.text == "")
        {
            inputFieldImage.color = Color.yellow;
            inputFieldPlaceholderText.text = "ENTER NAME PLEASE";
        }
        else
        {
            saveSystemManager.SaveFileToJson(inputField.text);
            saveSystemManager.ShowSaves();
            inputField.text = "";
        }
    }

    public void OnOverrideButtonPress()
    {
        saveSystemManager.SaveFileToJson(inputField.text);
        saveSystemManager.ShowSaves();
    }

    public void OnInputFieldChange()
    {
        if (saveSystemManager.DoesSaveNameExist(inputField.text))
        {
            inputFieldImage.color = Color.yellow;
            newSaveButton.SetActive(false);
            overrideSaveButton.SetActive(true);
        }
        else
        {
            inputFieldImage.color = Color.white;
            newSaveButton.SetActive(true);
            overrideSaveButton.SetActive(false);
        }
    }
}
