using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score = 0;
    
    public int GetScore()
    {
        return score;
    }

    public void ModifyScore(int amount)
    {
        score += amount;
        Mathf.Clamp(score, 0, int.MaxValue);
        Debug.Log("Score: " + score);
    }

    public void ResetScore()
    {
        score = 0;
    }
}
