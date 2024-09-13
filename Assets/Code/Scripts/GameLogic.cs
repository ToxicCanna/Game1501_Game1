using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    PRESSANDHOLD,
    TIMEDCOOKING,
    STACKER,
    RHYTHM
}

public class GameLogic : Singleton<GameLogic>
{
    [SerializeField] private int leeway; //leeway for this level
    [SerializeField] private GameMode currentSceneGameMode;
    //Structure basic game logic here. 

    public void RecordScore()
    {
        //tells ScoreManager to add a score. 
    }

    public void SetTargets()
    {
        //tells ScoreManager to set targets
    }
}
