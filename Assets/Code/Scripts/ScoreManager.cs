using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum Medal
{
    NONE,
    BRONZE,
    SILVER,
    GOLD
}

public class ScoreManager : Singleton<ScoreManager>
{
    List<int> playerPerformancePercentage = new List<int>();
    List<int> targetPercentage = new List<int>();
    [SerializeField] private int bronzeScore;
    [SerializeField] private int silverScore;
    [SerializeField] private int goldScore;

    public int CalculateScore(int leeway, GameMode gameMode)
    {
        //stacker gameMode is unique so make it a unique calc
        if (gameMode == GameMode.STACKER)
        {
            return CalcScoreAverage(leeway);
        }
        //calc the score here using playerPerformancePercentage and targetPercentage, within leeway it is perfect. 
        if (playerPerformancePercentage.Count == targetPercentage.Count)
        {
            return CalcScoreWithTargets(leeway);
        }

        return -4000; //error
    }

    public void AddTargetPercentage(int targetPercent)
    {
        //add code hint:1 simple line    
        targetPercentage.Add(targetPercent);
    }

    public void RecordPlayerPerformance(int playerPercent)
    {
        //add code same idea as above
        playerPerformancePercentage.Add(playerPercent);
    }

    public int CalcScoreWithTargets(int leeway)
    {
        int resultScore = 0;
        int numLength = 0;
        for (int i = 0; i < playerPerformancePercentage.Count(), i++)
        {
            int difference = targetPercentage[i] - playerPerformancePercentage[i];
            if (difference < leeway)
            {
                resultScore += 1000;
            }
            else
            {
                if (targetPercentage[i] >= 50)
                {
                    int numlineMax = targetPercentage[i] - leeway;
                    numLength = numlineMax;
                } 
                else
                {
                    int numlineMin = targetPercentage[i] + leeway;
                    numLength = 100 - numlineMin;
                }
                resultScore += 1000 - (difference / numLength);
            }
        }
        return resultScore;
    }

    public int CalcScoreAverage(int leeway) //gives a score based on how close given numbers are. 
    {
        int resultScore = 0;
        int numLength = 0;
        int scoreSum = playerPerformancePercentage.Sum();
        int avgScore = scoreSum / playerPerformancePercentage.Count();
        foreach (int performanceScore in playerPerformancePercentage)
        {
            int difference = avgScore - performanceScore;
            if (difference < leeway)
            {
                resultScore += 1000;
            }
            else
            {
                if (avgScore >= 50)
                {
                    int numlineMax = avgScore - leeway;
                    numLength = numlineMax;
                }
                else
                {
                    int numlineMin = avgScore + leeway;
                    numLength = 100 - numlineMin;
                }
                resultScore += 1000 - (difference / numLength);
            }
        }
        return resultScore;
    }

    public Medal GetPerformanceMedal(int leeway, GameMode gameMode)
    {
        int resultingScore = CalculateScore(leeway, gameMode);
        if (resultingScore >= goldScore)
        {
            return Medal.GOLD;
        }
        else if (resultingScore >= silverScore)
        {
            return Medal.SILVER;
        }
        else if (resultingScore >= bronzeScore)
        {
            return Medal.BRONZE;
        }
        else {
            return Medal.NONE;
        }
    }
}
