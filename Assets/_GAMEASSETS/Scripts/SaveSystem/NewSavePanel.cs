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
    [SerializeField] private Button playButton;
    [SerializeField] private Button deleteButton;

    private Button newSaveButtonBtn;
    
    private string currentTextInput;
    
    private void Start()
    {
        newSaveButtonBtn = newSaveButton.GetComponent<Button>();
        saveSystemManager = GameObject.Find("SaveSystemManager").GetComponent<SaveSystemManager>();
        if (saveSystemManager.CurrentSaveName != "")
        {
            inputField.text = saveSystemManager.CurrentSaveName; //set the inputField-Text to current save name if not empty
        }
    }

    private void Update()
    {
        if (currentTextInput != saveSystemManager.CurrentSaveName)
        {
            inputField.text = saveSystemManager.CurrentSaveName; //if the current active save name has changed, change the inputField to current save name
            currentTextInput = saveSystemManager.CurrentSaveName; //change the local current Text input to the new name from the manager to detect the change
        }

        if (saveSystemManager.CurrentSaveName != "" && saveSystemManager.DoesSaveNameExist(saveSystemManager.CurrentSaveName))
        {
            deleteButton.interactable = true; //if filename is not empty and does exist, activate delete button
        }
        else
        {
            deleteButton.interactable = false; //else disable button
        }

        newSaveButtonBtn.interactable = inputField.text != "";

        playButton.interactable = saveSystemManager.CurrentSaveName != ""; //if the current save file is not empty, activate play button
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

    public void OnPlayButtonPress()
    {
        //TODO: Load World with active Save
    }

    public void OnDeleteButtonPress()
    {
        saveSystemManager.DeleteSave(saveSystemManager.CurrentSaveName);
        saveSystemManager.CurrentSaveName = "";
    }
}
