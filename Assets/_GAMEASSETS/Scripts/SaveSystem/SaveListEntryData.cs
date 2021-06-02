using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveListEntryData : MonoBehaviour
{
    [SerializeField] private TMP_Text saveFileNameTXT;
    [SerializeField] private TMP_Text collectibleCountTXT;
    [SerializeField] private TMP_Text dateTXT;
    [SerializeField] private int currentSaveVersion;

    [SerializeField] private string saveName = "-undefined-";
    [SerializeField] private int collectibleCount = 0;
    [SerializeField] private int elementsCount = 0;
    [SerializeField] private int saveVersion = 1;
    private DateTime saveDate = DateTime.Now;
    private SaveSystemManager saveSystemManager;

    private void Start()
    {
        currentSaveVersion = saveSystemManager.CurrentSaveVersion;
        
        if (saveVersion < currentSaveVersion)
        {
            GetComponent<Image>().color = Color.red;
        } else if (saveVersion > currentSaveVersion)
        {
            GetComponent<Image>().color = Color.yellow;
        }
        
        saveFileNameTXT.text = saveName;

        string collectsString;
        collectsString = collectibleCount == 1 ? "Collect" : "Collects";
        
        string elementsString;
        elementsString = elementsCount == 1 ? "Element" : "Elements";
        
        collectibleCountTXT.text = collectibleCount + " " + collectsString + " | " + elementsCount + " " + elementsString;
        dateTXT.text = saveDate.ToString("g", CultureInfo.CurrentCulture);
    }

    public void SetValues(String saveNameIn, int collectibleCountIn, int elementsCountIn, int saveVersionIn, string saveDateIn, SaveSystemManager saveSystemManagerIn)
    {
        saveName = saveNameIn;
        collectibleCount = collectibleCountIn;
        elementsCount = elementsCountIn;
        saveVersion = saveVersionIn;
        saveDate = DateTime.Parse(saveDateIn);
        saveSystemManager = saveSystemManagerIn;
    }
}
