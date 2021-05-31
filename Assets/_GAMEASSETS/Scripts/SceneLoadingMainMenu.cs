using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //needed for using methods such as .LoadScene()

public class SceneLoadingMainMenu : MonoBehaviour
{
    [SerializeField]
    private string levelScene; //Storing the Level Scene we want to load. Serializing it to be able to set it in the Unity Inspector

    [SerializeField]
    private string optionsScene;//Storing the Options Scene, were the player can see the Controls. Serializing it to be able to set it in the Unity Inspector

    [SerializeField]
    private string creditsScene;//Storing the Credits Scene. Serializing it to be able to set it in the Unity Inspector

    private void OnEnable() // Special Unity Method, called automatically everytime a GO gets enabled
    {
        // .sceneLoaded is a so called event!
        // We want to react to that event, so we subscribe to it with a reaction method
        // This is called a "Callback"
        // We subscribe to the event with our callback with the += operator
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    
    private void OnDisable() // Special Unity Method, called automatically everytime a GO gets disabled
    {
        // .sceneLoaded is a so called event!
        // We want to react to that event, so we subscribe to it with a reaction method
        // This is called a "Callback"

        // Once you subscribe to an event you also have to unsubscribe to it at some point
        // If you won't you're opening up a can of worms and introduce potential
        // misbehaviour
        // In order to prevent that we're unsubscribing from events
        // We unsubscribe from the event with our callback with the -= operator
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Our event Callback
    /// Called everytime a scene is loaded successfully
    /// Needs a scene and a loadscenemode as arguments
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // We just log the name of the scene to check if the callback works
        Debug.Log("Scene: " + scene.name + "was loaded successfully");
    }


    // The .LoadScene() method is used to load the scene we want
    //to know which scene to load we insert our string variable (the levels name)
    public void LoadLevelScene()
    {
        SceneManager.LoadScene(levelScene);
    }

    public void LoadOptionsScene()
    {
        SceneManager.LoadScene(optionsScene);
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene(creditsScene);
    }
}
