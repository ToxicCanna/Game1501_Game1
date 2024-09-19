using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //this is just a hub for communicating for now. 
    private int gameplayPlayerPercentValue;
    private int leeway;
    [SerializeField] private GameplayUI gameUI;

    void Start()
    {
        leeway = GameLogic.Instance.getLeeway();
    }
    //get the Gamelogic and commands it
    //if need to grab and pass UIButtonInfo to GameLogic 

    public void RevealState(int currentPercent, int currentTarget) //this asks the UI to give player information
    {
        Debug.Log("gamemanager tells UI to take a peek, current Percent = " + currentPercent);
        gameplayPlayerPercentValue = currentPercent;
        gameUI.RecievePeek(gameplayPlayerPercentValue, currentTarget, leeway);
    }

    public void ProgressState(int currentPercent, int currentTarget) //this transitions the UI to the next part of the game
    {
        Debug.Log("GM telling UI to progress to the next one");
        gameUI.RecieveProgress(currentPercent, currentTarget, leeway);
    }

    //When calling this function use the scene index in build settings to call specific scenes, EX. ChangeLevel(0) for main menu
    public void ChangeLevel(int buildIndex)
    {
        Debug.Log("Changing Scene to " + buildIndex);
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
