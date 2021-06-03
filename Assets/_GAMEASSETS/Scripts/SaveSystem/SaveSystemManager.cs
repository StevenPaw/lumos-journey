using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class SaveSystemManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject saveListContent;
    [SerializeField] private GameObject saveListEntry;
    [SerializeField] private int currentSaveVersion;

    private string fileEnding = ".gamesave"; //the fileEnding used for the saveFiles
    private string currentSaveName; //The name of the current active saveFile
    
    private string[] filePaths; //The paths to all gameSaves as array
    private string saveFilePath; //The path where the saveFiles are stored

    public PlayerData PlayerData
    { get => playerData; set => playerData = value; }

    public int CurrentSaveVersion
    { get => currentSaveVersion; set => currentSaveVersion = value; }

    public string CurrentSaveName
    { get => currentSaveName; set => currentSaveName = value; }

    private void Awake()
    {
        saveFilePath = Application.persistentDataPath;
    }

    private void Start()
    {
        ShowSaves();
    }

    
    //SAVING
    /// <summary>
    /// Saves the current playerData to the file with the given saveName
    /// </summary>
    /// <param name="saveName">(string) name of saveFile</param>
    public void SaveFileToJson(string saveName)
    {
        playerData.saveVersion = currentSaveVersion;
        playerData.dateTimeOfSave = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        string playerDataToSave = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFilePath + "/" + saveName + fileEnding, playerDataToSave);
    }
    
    
    //SHOWING
    /// <summary>
    /// Show all Save Files in List of SaveFilePrefabs
    /// </summary>
    public void ShowSaves()
    {
        GetAllSaveFilePaths();

        //First destroy all save buttons
        if (saveListContent.transform.childCount > 0)
        {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in saveListContent.transform) 
            {
                children.Add(child.gameObject);
            }
            children.ForEach(Destroy);
        }

        List<PlayerData> saveFiles = new List<PlayerData>();
        List<string> saveFileNames = new List<string>();
        
        //then create new ones based on the files
        foreach (String saveFile in filePaths)
        {

            saveFiles.Add(LoadFromJson(Path.GetFileNameWithoutExtension(saveFile)));
            saveFileNames.Add(Path.GetFileNameWithoutExtension(saveFile));
        }

        for(int i = 0; i < saveFiles.Count; i++)
        {
            GameObject entry = Instantiate(saveListEntry, saveListContent.transform, true); //Create an entry for the saves List
            SaveListEntryData saveListEntryData = entry.GetComponent<SaveListEntryData>();

            entry.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            Vector3 position = entry.transform.position;
            entry.transform.localPosition = new Vector3(position.x, position.y, 0f);
            
            saveListEntryData.SetValues(saveFileNames[i], saveFiles[i].collectedCollectables, 
                saveFiles[i].collectedElements, saveFiles[i].saveVersion, saveFiles[i].dateTimeOfSave, this);
        }
    }
    
    /// <summary>
    /// Get all File Paths of all Saves and save them into array
    /// </summary>
    private void GetAllSaveFilePaths()
    {
        filePaths = Directory.GetFiles(saveFilePath, "*" + fileEnding); //Gets all paths to files that end with ".gamesave"(fileEnding)
    }
    
    
    //LOADING
    /// <summary>
    /// Load Gamesave from JSON-File with the given name
    /// </summary>
    /// <param name="saveName">(string) save to load</param>
    /// <returns></returns>
    private PlayerData LoadFromJson(string saveName)
    {
        PlayerData playerDataOut = new PlayerData();
        if (File.Exists(saveFilePath + "/" + saveName + fileEnding))
        {
            string loadedPlayerData = File.ReadAllText(saveFilePath + "/" + saveName + fileEnding);
            playerDataOut = JsonUtility.FromJson<PlayerData>(loadedPlayerData);
        }
        else
        {
            Debug.Log("No PlayerData SaveFile for " + saveName + " found!");
        }

        return playerDataOut;
    }

    /// <summary>
    /// Load specific save file and put data into active playerData
    /// </summary>
    /// <param name="saveName">(string) save to load to gameState</param>
    public void LoadSaveFileToGameState(string saveName)
    {
        playerData = LoadFromJson(saveName);
        currentSaveName = saveName;
    }

    /// <summary>
    /// Checks if a specific save file exists and returns the result as bool
    /// </summary>
    /// <param name="saveName">(string) saveName to check for</param>
    /// <returns></returns>
    public bool DoesSaveNameExist(string saveName)
    {
        bool doesExist = false;

        GetAllSaveFilePaths();
        foreach (string save in filePaths)
        {
            string fileName = Path.GetFileNameWithoutExtension(save);
            if (fileName == saveName)
            {
                doesExist = true;
            }
        }
        
        return doesExist;
    }
}
