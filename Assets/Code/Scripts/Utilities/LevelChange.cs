using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this levelchanger is for the menu scenes, as including the interconnected scripts for the menu is largely unneeded

public class LevelChange : MonoBehaviour
{
    //When calling this function use the scene index in build settings to call specific scenes, EX. ChangeLevel(0) for main menu
    public void SceneSwitch(int buildIndex)
    {
        print("Changing Scene");
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
    }


    //call if game is to quit, in editor this will close playmode, in a build it will close game
    public void QuitGame()
    {
        Debug.Log("GameManager is quitting the game");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the Editor
#else
        Application.Quit();
#endif
    }
}