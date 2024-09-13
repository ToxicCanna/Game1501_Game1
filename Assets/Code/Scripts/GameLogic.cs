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

    int currentRepetition;
    [SerializeField] int targetRepetition;

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

        switch (currentSceneGameMode)
        {
            case GameMode.PRESSANDHOLD:
                break;
            case GameMode.TIMEDCOOKING:
                break;
            case GameMode.STACKER:
                break;
            case GameMode.RHYTHM:
                currentTimedPercent = 0;
                break;
        }

    }

    public void Update()
    {
        currentTime = Time.time;
        switch (currentSceneGameMode)
        {
            case GameMode.PRESSANDHOLD:
                break;
            case GameMode.TIMEDCOOKING:
                break;
            case GameMode.STACKER:
                break;
            case GameMode.RHYTHM:
                countingUpwards = ((int)(((currentTime - startTime) * gameSpeed) / 100) % 2) == 0;
                currentTimedPercent = (int)((currentTime - startTime) * gameSpeed) % 100;
                //Debug.Log(countingUpwards);
                //Debug.Log((countingUpwards) ? currentTimedPercent : 100-currentTimedPercent);
                break;
        }

    }

    public void ButtonHeld(ButtonInfo heldInfo)
    {
        //Debug.Log("button Held");
    }

    public void ButtonPressed(ButtonInfo pressedInfo)
    {
        switch (currentSceneGameMode)
        {
            case GameMode.PRESSANDHOLD:
                break;
            case GameMode.TIMEDCOOKING:
                break;
            case GameMode.STACKER:
                break;
            case GameMode.RHYTHM:
                Debug.Log((countingUpwards) ? currentTimedPercent : 100 - currentTimedPercent); 
                //record value here and send both this and currentTimedPercent to scoremanager note score manager may want the countingupward, if upward you want the number small
                currentRepetition++;
                break;
        }

    }

    public void ButtonReleased(ButtonInfo releasedInfo)
    {
        Debug.Log("buttonReleased");

        switch (currentSceneGameMode)
        {
            case GameMode.PRESSANDHOLD:
                break;
            case GameMode.TIMEDCOOKING:
                break;
            case GameMode.STACKER:
                break;
            case GameMode.RHYTHM:
                Debug.Log((countingUpwards) ? currentTimedPercent : 100 - currentTimedPercent);
                //record value here and send both this and currentTimedPercent to scoremanager
                currentRepetition++;
                break;
        }

    }
}
