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
    [SerializeField] private float tapLeeway; //a small int, if within this we will say the player didn't hold long enough. In stacker mode used to prevent button presses being close togeher
    [SerializeField] private GameMode currentSceneGameMode;
    //Structure basic game logic here. 

    [SerializeField] private float gameSpeed = 15f;

    private float startTime;
    private float currentTime;
    private float buttonPressedTime;
    private float previousButtonPressTime;

    private int currentTimedPercent;
    private int currentPlayerPercent;
    private bool countingUpwards;

    private int currentRepetition;
    private int targetRepetition; //this is actually just targetValues.count
    [SerializeField] private int[] targetValues;

    private int tempRecord;
    private bool heldRecorded;

    private bool gameOver;

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
        gameOver = false;
        FindObjectOfType<PlayerInputs>().RegisterListener(this);
        SetTargets();
        StartMinigame(); //move this to ui later
        heldRecorded = false;
        currentRepetition = 0;
        targetRepetition = targetValues.Length;
        countingUpwards = true; //this is currently true every gamemode
        currentTimedPercent = 0;
        
    }

    public void StartMinigame()
    { 
        startTime = Time.time;
        previousButtonPressTime = -tapLeeway;
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
                    currentPlayerPercent = Mathf.Clamp((int)((currentTime - buttonPressedTime) * gameSpeed), 0, 100);
                    GameManager.Instance.revealState(currentPlayerPercent, getCurrentTarget());
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
                        if(!gameOver)
                        {
                            GameManager.Instance.progressState(currentPlayerPercent, getCurrentTarget());
                        }

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
                if (buttonPressedTime - previousButtonPressTime > tapLeeway) //have a cooldown to stop mashing
                {
                    previousButtonPressTime = buttonPressedTime;
                    Debug.Log((countingUpwards) ? currentTimedPercent : 100 - currentTimedPercent);
                    RecordScore((countingUpwards) ? currentTimedPercent : 100 - currentTimedPercent);
                    checkRepetition(); 
                }
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
                if (!gameOver)
                {
                    GameManager.Instance.progressState(currentTimedPercent, getCurrentTarget());
                }
                RecordScore(currentTimedPercent);
                checkRepetition();
                break;
            case GameMode.TIMEDCOOKING:
                heldRecorded = false;
                if(currentTime - buttonPressedTime < tapLeeway)
                { //player Tapped
                    //tells UI to reveal percent
                    currentPlayerPercent = Mathf.Clamp(currentTimedPercent, 0, 100);
                    GameManager.Instance.revealState(currentPlayerPercent, getCurrentTarget());
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
        gameOver = true;
        Debug.Log("minigames done");
        Debug.Log(ScoreManager.Instance.GetPerformanceMedal(leeway, currentSceneGameMode));
    }

    public void checkRepetition() //checks if player done the task enough reps
    {
        if (currentRepetition >= targetRepetition)
        {
            EndMinigame();
        }
    }

    public int getLeeway()
    {
        return leeway;
    }

    public int getCurrentTarget()
    {
        return targetValues[currentRepetition];
    }
}
