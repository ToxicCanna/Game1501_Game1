using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    List<int> playerPerformancePercentage = new List<int>();
    List<int> targetPercentage = new List<int>();

    public int CalculateScore(int leeway)
    {
        //calc the score here using playerPerformancePercentage and targetPercentage, within leeway it is perfect. 
        if (playerPerformancePercentage.Count == targetPercentage.Count)
        { 
            
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
}
