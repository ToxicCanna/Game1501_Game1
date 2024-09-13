using System;
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

public class GameLogic : Singleton<GameLogic>, IButtonListener
{
    [SerializeField] private int leeway; //leeway for this level
    [SerializeField] private GameMode currentSceneGameMode;
    //Structure basic game logic here. 

    [SerializeField] private float gameSpeed = 15f;

    float startTime;
    float currentTime;

    int currentTimedPercent;
    int currentPlayerPercent;
    bool countingUpwards;

    public void RecordScore()
    {
        //tells ScoreManager to add a score. 
    }

    public void SetTargets()
    {
        //tells ScoreManager to set targets
    }

    public void Start()
    {
        FindObjectOfType<PlayerInputs>().RegisterListener(this);
        startTime = Time.time;
        countingUpwards = true;
        if (currentSceneGameMode == GameMode.RHYTHM)
        {
            currentTimedPercent = 0;
        }
    }

    public void Update()
    {
        currentTime = Time.time;

        if (currentSceneGameMode == GameMode.RHYTHM)
        {
            countingUpwards = ((int)(((currentTime - startTime) * gameSpeed)/100) %2 )== 0;
            currentTimedPercent = (int)((currentTime - startTime)* gameSpeed)%100;
            //Debug.Log(countingUpwards);
            //Debug.Log((countingUpwards) ? currentTimedPercent : 100-currentTimedPercent);



        }
    }

    public void ButtonHeld(ButtonInfo heldInfo)
    {
        //Debug.Log("button Held");
    }

    public void ButtonPressed(ButtonInfo pressedInfo)
    {
        if (currentSceneGameMode == GameMode.RHYTHM)
        {
            //record value here and send both this and currentTimedPercent to scoremanager
            Debug.Log((countingUpwards) ? currentTimedPercent : 100 - currentTimedPercent);
        }
    }

    public void ButtonReleased(ButtonInfo releasedInfo)
    {
        Debug.Log("buttonReleased");
        if (currentSceneGameMode == GameMode.RHYTHM)
        {
            //record value here and send both this and currentTimedPercent to scoremanager
            Debug.Log((countingUpwards) ? currentTimedPercent : 100 - currentTimedPercent);
        }
    }
}
