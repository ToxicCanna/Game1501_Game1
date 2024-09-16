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
    [SerializeField] private float tapLeeway; //a small int, if within this we will say the player didn't hold long enough
    [SerializeField] private GameMode currentSceneGameMode;
    //Structure basic game logic here. 

    [SerializeField] private float gameSpeed = 15f;

    private float startTime;
    private float currentTime;
    private float buttonPressedTime;

    private int currentTimedPercent;
    private int currentPlayerPercent;
    private bool countingUpwards;

    private int currentRepetition;
    private int targetRepetition; //this is actually just targetValues.count
    [SerializeField] private int[] targetValues;

    private int tempRecord;
    private bool heldRecorded;

    public void RecordScore(int playerPercent)
    {
        //tells ScoreManager to add a score. 
        ScoreManager.Instance.RecordPlayerPerformance(playerPercent);
        currentRepetition++;
    }

    public void SetTargets()
    {
        //tells ScoreManager to set targets
        //note that for Stackers, the targets are not used and can be anything. 
        foreach (int targetValue in targetValues)
        {
            ScoreManager.Instance.AddTargetPercentage(targetValue);
        }
    }

    public void Start()
    {
        heldRecorded = false;
        currentRepetition = 0;
        FindObjectOfType<PlayerInputs>().RegisterListener(this);
        startTime = Time.time;
        SetTargets();
        targetRepetition = targetValues.Length;
        countingUpwards = true; //this is currently true every gamemode

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
                currentTimedPercent = (int)((currentTime - startTime) * gameSpeed);
                break;
            case GameMode.STACKER:
                countingUpwards = ((int)(((currentTime - startTime) * gameSpeed) / 100) % 2) == 0;
                currentTimedPercent = (int)((currentTime - startTime) * gameSpeed) % 100;
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
        if (!heldRecorded)
        {
            switch (currentSceneGameMode)
            {
                case GameMode.PRESSANDHOLD:
                    break;
                case GameMode.TIMEDCOOKING:
                    if (currentTime - buttonPressedTime > tapLeeway) //player held and not tapping
                    {
                        currentPlayerPercent = Mathf.Clamp(currentTimedPercent, 0, 100);
                        //Debug.Log("recorded: " + currentPlayerPercent);
                        RecordScore(currentPlayerPercent);
                        heldRecorded = true;
                        checkRepetition();
                        startTime = Time.time;
                    }
                    break;
                case GameMode.STACKER:
                    break;
                case GameMode.RHYTHM:
                    break;
            }
        }
    }

    public void ButtonPressed(ButtonInfo pressedInfo)
    {
        buttonPressedTime = Time.time;
        switch (currentSceneGameMode)
        {
            case GameMode.PRESSANDHOLD:
                break;
            case GameMode.TIMEDCOOKING:
                break;
            case GameMode.STACKER:
                Debug.Log((countingUpwards) ? currentTimedPercent : 100 - currentTimedPercent);
                RecordScore((countingUpwards) ? currentTimedPercent : 100 - currentTimedPercent);
                checkRepetition();
                break;
            case GameMode.RHYTHM:
                Debug.Log((countingUpwards) ? currentTimedPercent : 100 - currentTimedPercent);
                //record value here and send both this and currentTimedPercent to scoremanager note score manager may want the countingupward, if upward you want the number small
                //record both score on release, have a check there for tap
                tempRecord = (countingUpwards) ? currentTimedPercent : 100 - currentTimedPercent;
                break;
        }

    }

    public void ButtonReleased(ButtonInfo releasedInfo)
    {
        switch (currentSceneGameMode)
        {
            case GameMode.PRESSANDHOLD:
                currentTimedPercent = (int)((currentTime - buttonPressedTime) * gameSpeed);
                currentTimedPercent = Mathf.Clamp(currentTimedPercent, 0, 100);
                Debug.Log("PressAndHold Score: " + currentTimedPercent);
                RecordScore(currentTimedPercent);
                currentRepetition++;
                checkRepetition();
                break;
            case GameMode.TIMEDCOOKING:
                heldRecorded = false;
                if(currentTime - buttonPressedTime < tapLeeway)
                { //player Tapped
                    //tells UI to reveal percent
                    currentPlayerPercent = Mathf.Clamp(currentTimedPercent, 0, 100);
                    GameManager.Instance.revealState(currentPlayerPercent);
                }
                break;
            case GameMode.STACKER:
                break;
            case GameMode.RHYTHM:
                //Debug.Log((countingUpwards) ? currentTimedPercent : 100 - currentTimedPercent);
                //record value here and send both this and currentTimedPercent to scoremanager
                if (currentTime - buttonPressedTime > tapLeeway) //lets prevent recording tapping
                { 
                    RecordScore(tempRecord);
                    RecordScore((countingUpwards) ? currentTimedPercent : 100 - currentTimedPercent);
                }
                checkRepetition();
                break;
        }

    }

    public void EndMinigame()
    {
        Debug.Log("minigames done");
    }

    public void checkRepetition() //checks if player done the task enough reps
    {
        if (currentRepetition >= targetRepetition)
        {
            EndMinigame();
        }
    }
}
