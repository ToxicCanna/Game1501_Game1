using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //this is just a hub for communicating for now. 
    private int gameplayPlayerPercentValue;

    //get the Gamelogic and commands it
    //if need to grab and pass UIButtonInfo to GameLogic 

    public void revealState(int currentPercent)
    {
        Debug.Log("gamemanager tells UI to do something, current Percent = " + currentPercent);
        gameplayPlayerPercentValue = currentPercent;
    }

}
