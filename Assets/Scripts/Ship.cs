using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private int score = 0;

    [SerializeField] private int targetFPS = 30;
    [SerializeField] private Text scoreText = null;
    [SerializeField] private Text HighScoreText = null;

    private void Start()
    {
        HighScoreText.text = @"High
Score
_____
" +
            PlayerPrefs.GetInt("HighScore", 0).ToString();

        Application.targetFrameRate = 30;
    }

    private void Update()
    {
        if (Application.targetFrameRate != targetFPS)
        {
            Application.targetFrameRate = targetFPS;
        }
    }

    public int GetScore()
    { 
        return score;
    }

    public void AddToScore(int s)
    {
        score += s;
        scoreText.text = "SCORE: " + score.ToString();
        HighScoreCheck();
    }

    private void HighScoreCheck()
    {
        if(score > PlayerPrefs.GetInt("HighScore",0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
