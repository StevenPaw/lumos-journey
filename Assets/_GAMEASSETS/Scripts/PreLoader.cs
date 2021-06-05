using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreLoader : MonoBehaviour
{
    [SerializeField] private float timeDelayInSeconds;
    [SerializeField] private string sceneToLoadAfterPreload;

    void Update()
    {
        if (timeDelayInSeconds > 0)
        {
            timeDelayInSeconds -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(sceneToLoadAfterPreload, LoadSceneMode.Single);
        }
    }
}
