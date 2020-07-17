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
    }

    private void Update()
    {
        if(Application.targetFrameRate != targetFPS)
        {
            Application.targetFrameRate = targetFPS;
        }
    }

    public int GetScore()
    {
        if(transform.position.z > score)
        {
            score += Mathf.RoundToInt(Time.deltaTime);
        }

        return score;
    }

   
}
