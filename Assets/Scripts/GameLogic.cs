using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    private int score;
    private int maxScore;
    [SerializeField] private Text textScore;
    [SerializeField] private Text textMaxScore;
    public void IncreaseScore()
    {
        score++;
        textScore.text = score.ToString();
        
    }
    public void ResetScore()
    {
        maxScore=Mathf.Max(score, maxScore); //if (score > maxScore) maxScore = score;
        score = 0;
        textMaxScore.text = maxScore.ToString();        
        textScore.text = score.ToString();
    }

}
