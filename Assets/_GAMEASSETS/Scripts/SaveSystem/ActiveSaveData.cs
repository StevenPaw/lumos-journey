using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[DisallowMultipleComponent]
public class ActiveSaveData : MonoBehaviour
{
    [Header("There should only be one of this script in all times!")]
    [SerializeField] private PlayerData playerData;

    private void Start()
    {
        //Making sure there is only one class of ActiveSaveData available at a time:
        if (GameObject.Find("ActiveSaveData") != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    /// <summary>
    /// Set the Data of the currently active Savefile
    /// </summary>
    /// <param name="playerDataIn"></param>
    public void SetData(PlayerData playerDataIn)
    {
        playerData = playerDataIn;
    }
    
    /// <summary>
    /// Get the data of the current active SaveData
    /// </summary>
    /// <returns>(PlayerData) active SaveData-Variables</returns>
    public PlayerData GetData()
    {
        if (playerData != null)
        {
            return playerData;
        }
        //If there is no SaveData stored yet (for example in a new Game) use a new PlayerData-Class
        playerData = new PlayerData();
        return playerData;
    }
}
