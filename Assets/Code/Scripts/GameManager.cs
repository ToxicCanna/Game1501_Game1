using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void revealState(int currentPercent, int currentTarget)
    {
        Debug.Log("gamemanager tells UI to take a peek, current Percent = " + currentPercent);
        gameplayPlayerPercentValue = currentPercent;
        gameUI.RecievePeek(gameplayPlayerPercentValue, currentTarget, leeway);
    }

    public void progressState(int currentPercent, int currentTarget)
    {
        Debug.Log("GM telling UI to progress to the next one");
        gameUI.RecieveProgress(currentPercent, currentTarget, leeway);
    }

}
