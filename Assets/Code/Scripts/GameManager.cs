using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //this is just a hub for communicating for now. 
    public int gameplayPlayerPercentValue;
    private bool increasing = true;
    private bool spaceHeld = false;
    public float speedMultiplier = 1.0f;
    private float updateInterval = 0.1f;
    private float nextUpdateTime = 0f;
    private float holdTime = 0;
    private void Start()
    {
        GameLogic.Instance.SetupLevel();
    }
    public void UpdatePerformance()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            spaceHeld = true;
            holdTime += Time.deltaTime;
            float variableSpeed = Mathf.Clamp(holdTime * speedMultiplier, .1f, 1f);
            if (Time.time > nextUpdateTime)
            {
                nextUpdateTime = Time.time + updateInterval;

                if (increasing)
                {
                    gameplayPlayerPercentValue += Mathf.RoundToInt(variableSpeed);
                    if (gameplayPlayerPercentValue >= 100)
                    {
                        gameplayPlayerPercentValue = 100;
                        increasing = false;
                    }
                }
                else
                {
                    gameplayPlayerPercentValue -= Mathf.RoundToInt(variableSpeed);
                    if (gameplayPlayerPercentValue <= 0)
                    {
                        gameplayPlayerPercentValue = 0;
                        increasing = true;
                    }
                }
            }
        }
    }

    public void RecordPerformance()
    {
        spaceHeld = false;
        holdTime = 0f;
        Debug.Log("Player released space value-->" + gameplayPlayerPercentValue);
        ScoreManager.Instance.RecordPlayerPerformance(gameplayPlayerPercentValue);
        
        GameLogic.Instance.RecordScore();
        gameplayPlayerPercentValue = 0;
        increasing = true;
    }
    //get the Gamelogic and commands it
    //if need to grab and pass UIButtonInfo to GameLogic 
}
