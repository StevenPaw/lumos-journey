using System;
using System.Collections.Generic;
using System.IO;
using _GAMEASSETS.Scripts.SaveSystem;
using UnityEngine;

public class SaveSystemManager : MonoBehaviour
{
    //plan: Save all data with ".gamesave" as ending for user saves with the name that the player enters
    //also use ".preferences" for the preferences of the player like volume or other settings to set them globally
    //save also a gamesave-version with the save to make sure its up to date
    //save also a date and time when the save was last overridden to make it findable easier

    [SerializeField] private PlayerData playerData;
    //[SerializeField] private GameObject playerGO;
    [SerializeField] private GameObject saveListContent;
    [SerializeField] private GameObject saveListEntry;
    [SerializeField] private int currentSaveVersion;

    private string fileEnding = ".gamesave";
    private string currentSaveName;
    
    private string[] filePaths;
    private string saveFilePath;

    private Camera mainCamera;

    public PlayerData PlayerData
    { get => playerData; set => playerData = value; }

    public int CurrentSaveVersion
    { get => currentSaveVersion; set => currentSaveVersion = value; }
    
    void Start()
    {
        mainCamera = Camera.main;
        
        saveFilePath = Application.persistentDataPath;
        
        ShowSaves();
    }

    
    //SAVING
    private void SaveToJSON(string saveName)
    {
        string playerDataToSave = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFilePath + "/" + saveName + fileEnding, playerDataToSave);
    }
    
    public void SaveFile(String saveName)
    {
        playerData.saveVersion = currentSaveVersion;
        playerData.dateTimeOfSave = DateTime.Now.ToString();
        SaveToJSON(saveName);
    }

    
    //SHOWING
    public void ShowSaves()
    {
        GetAllSaveFilenames();

        //First destroy all save buttons
        if (saveListContent.transform.childCount > 0)
        {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in saveListContent.transform) 
            {
                children.Add(child.gameObject);
            }
            children.ForEach(child => Destroy(child));
        }

        List<PlayerData> saveFiles = new List<PlayerData>();
        List<string> saveFileNames = new List<string>();
        
        //then create new ones based on the files
        foreach (String saveFile in filePaths)
        {
            PlayerData tempPlayerData = new PlayerData();

            saveFiles.Add(LoadFromJSON(Path.GetFileNameWithoutExtension(saveFile)));
            saveFileNames.Add(Path.GetFileNameWithoutExtension(saveFile));
        }

        for(int i = 0; i < saveFiles.Count; i++)
        {
            GameObject entry = Instantiate(saveListEntry); //Create an entry for the saves List
            entry.transform.SetParent(saveListContent.transform); //Set the save list as parent
            SaveListEntryData saveListEntryData = entry.GetComponent<SaveListEntryData>();

            entry.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            entry.transform.localPosition = new Vector3(entry.transform.position.x, entry.transform.position.y, 0f);
            
            saveListEntryData.SetValues(saveFileNames[i], saveFiles[i].collectedCollectables, 
                saveFiles[i].collectedElements, saveFiles[i].saveVersion, saveFiles[i].dateTimeOfSave, this);
        }
    }
    
    private void GetAllSaveFilenames()
    {
        filePaths = Directory.GetFiles(saveFilePath, "*" + fileEnding); //Gets all paths to files that end with ".gamesave"(fileEnding)
    }
    
    
    //LOADING
    private PlayerData LoadFromJSON(string saveName)
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

    public void MoveToLoadedPosition()
    {
        //playerGO.transform.position = playerData.playerPosition;
    }

    public bool DoesSaveNameExist(string saveName)
    {
        bool doesExist = false;

        GetAllSaveFilenames();
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
