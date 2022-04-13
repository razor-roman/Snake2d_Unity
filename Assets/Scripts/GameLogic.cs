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
        if (score > maxScore) maxScore = score;
        textMaxScore.text = maxScore.ToString();
        score = 0;
        textScore.text = score.ToString();
    }

}
