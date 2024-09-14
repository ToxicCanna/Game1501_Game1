using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    List<int> playerPerformancePercentage = new List<int>();
    List<int> targetPercentage = new List<int>();

    public int CalculateScore(int leeway)
    {
        int totalScore = 0;

        if (playerPerformancePercentage.Count == 0 || targetPercentage.Count == 0)
        {
            Debug.Log("No player performance or target percentages");
            return totalScore;
        }

        // Calculate the score for each player performance
        foreach (int playerPerformance in playerPerformancePercentage)
        {
            int chosenTarget = GetClosestTarget(playerPerformance);
            int difference = Mathf.Abs(playerPerformance - chosenTarget);
            //if difference is less or equal to leeway give full score else subtract the difference
            int score = (difference <= leeway) ? 100 : Mathf.Max(0, 100 - difference);
            totalScore += score;

            Debug.Log($"Performance: {playerPerformance}, Closest Target: {chosenTarget}, Difference: {difference}, Score: {score}");
        }

        return totalScore;
    }

    // Choose the target based on the closest performance value
    private int GetClosestTarget(int playerPerformance)
    {
        int closestTarget = targetPercentage[0];
        int minDifference = Mathf.Abs(playerPerformance - closestTarget);

        foreach (int target in targetPercentage)
        {
            int difference = Mathf.Abs(playerPerformance - target);
            if (difference < minDifference)
            {
                minDifference = difference;
                closestTarget = target;
            }
        }

        return closestTarget;
    }

    public void AddTargetPercentage(int targetPercent)
    {
        //add code hint:1 simple line
        targetPercentage.Add(targetPercent);
    }

    public void RecordPlayerPerformance(int playerPercent)
    {
        playerPerformancePercentage.Add(playerPercent);
        //add code same idea as above
    }
}
