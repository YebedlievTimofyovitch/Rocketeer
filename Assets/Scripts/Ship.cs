using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private int score = 0;

    [SerializeField] private int targetFPS = 30;
    [SerializeField] private Text scoreText = null;

    private void Start()
    {
        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 0;
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
    }
}
