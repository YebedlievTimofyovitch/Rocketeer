using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private int score = 0;

    [SerializeField] private int targetFPS = 30;

    private void Start()
    {
        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 0;
        StartCoroutine(TrackScore());
    }

    private void Update()
    {
        if (Application.targetFrameRate != targetFPS)
        {
            Application.targetFrameRate = targetFPS;
        }
    }

    private IEnumerator TrackScore()
    {
        while (true) { }
    }

    public int GetScore()
    { 
        return score;
    }

    public void AddToScore(int s)
    {
        score += s;
    }

   
}
