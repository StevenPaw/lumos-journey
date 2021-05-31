using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystemManager : MonoBehaviour
{
    //plan: Save all data with ".gamesave" as ending for user saves with the name that the player enters
    //also use ".preferences" for the preferences of the player like volume or other settings to set them globally
    //save also a gamesave-version with the save to make sure its up to date
    //save also a date and time when the save was last overridden to make it findable easier
    //

    private string[] filePaths;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        GetAllSaveFilenames();
        
        for (int i = 0; i < filePaths.Length; i++)
        {
            Debug.Log("Path: " + filePaths[i]);
        }
    }

    private void GetAllSaveFilenames()
    {
        filePaths = Directory.GetFiles(Application.persistentDataPath, "*.gamesave"); //Gets all paths to files that end with ".gamesave"
    }
}
