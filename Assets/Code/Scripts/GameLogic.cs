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
    public void SetupLevel()
    {
        List<int> targetPercentages = new List<int> { 50,60 }; // Example targets

        SetTargets(targetPercentages);

        InitializeLevelSettings();
    }
    public void RecordScore()
    {
        int score = ScoreManager.Instance.CalculateScore(leeway);
        //tells ScoreManager to add a score. 
    }

    public void SetTargets(List<int> targetPercentages)
    {
        foreach (var target in targetPercentages)
        {
            ScoreManager.Instance.AddTargetPercentage(target);
        }

        //tells ScoreManager to set targets
    }
    public void InitializeLevelSettings()
    {
        switch (currentSceneGameMode)
        {
            case GameMode.PRESSANDHOLD:
                leeway = 5; // example value for PRESSANDHOLD
                break;
            case GameMode.TIMEDCOOKING:
                leeway = 10; // example value for TIMEDCOOKING
                break;
            case GameMode.STACKER:
                leeway = 3; // example value for STACKER
                break;
            case GameMode.RHYTHM:
                leeway = 2; // example value for RHYTHM
                break;
            default:
                leeway = 5;
                break;
        }
    }
 }
