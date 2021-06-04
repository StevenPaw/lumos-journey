using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveListEntryData : MonoBehaviour
{
    [SerializeField] private TMP_Text saveFileNameTxt;
    [SerializeField] private TMP_Text collectibleCountTxt;
    [SerializeField] private TMP_Text dateTxt;
    [SerializeField] private int currentSaveVersion;

    [SerializeField] private string saveName = "-undefined-";
    private int collectibleCount;
    private int elementsCount;
    private int saveVersion = 1;
    private DateTime saveDate = DateTime.Now;
    private SaveSystemManager saveSystemManager;

    private Image buttonImage;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        
        currentSaveVersion = saveSystemManager.CurrentSaveVersion;
        
        if (saveVersion < currentSaveVersion)
        {
            buttonImage.color = Color.red;
        } else if (saveVersion > currentSaveVersion)
        {
            buttonImage.color = Color.yellow;
        }
        
        saveFileNameTxt.text = saveName;

        string collectsString = collectibleCount == 1 ? "Collect" : "Collects";
        
        string elementsString = elementsCount == 1 ? "Element" : "Elements";
        
        collectibleCountTxt.text = collectibleCount + " " + collectsString + " | " + elementsCount + " " + elementsString;
        dateTxt.text = saveDate.ToString("g", CultureInfo.CurrentCulture);
    }

    private void Update()
    {
        if (saveSystemManager.CurrentSaveName == saveName)
        {
            buttonImage.color = Color.green;
        }
        else
        {
            buttonImage.color = Color.white;
        }
    }

    /// <summary>
    /// Set Values for the SaveListEntry to show in the list
    /// </summary>
    /// <param name="saveNameIn">(string) name of the saveFile</param>
    /// <param name="collectibleCountIn">(int) count of collectibles collected</param>
    /// <param name="elementsCountIn">(int) count of elements collected</param>
    /// <param name="saveVersionIn">(int) version of the savefile</param>
    /// <param name="saveDateIn">(string) SaveDate-Value parsed as string from the date of the save</param>
    /// <param name="saveSystemManagerIn">(SaveSystemManager) to get a reference to the correct SaveSystemManager without GameObject.Find()</param>
    public void SetValues(string saveNameIn, int collectibleCountIn, int elementsCountIn, int saveVersionIn, string saveDateIn, SaveSystemManager saveSystemManagerIn)
    {
        saveName = saveNameIn;
        collectibleCount = collectibleCountIn;
        elementsCount = elementsCountIn;
        saveVersion = saveVersionIn;
        saveDate = DateTime.Parse(saveDateIn);
        saveSystemManager = saveSystemManagerIn;
    }

    /// <summary>
    /// Public Method to load save when clicking button
    /// </summary>
    public void OnLoadClick()
    {
        saveSystemManager.LoadSaveFileToGameState(saveName);
    }
}
