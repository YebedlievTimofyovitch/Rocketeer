using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private int score = 0;

    public int GetScore()
    {
        if(transform.position.z > score)
        {
            score += Mathf.RoundToInt(Time.deltaTime);
        }

        return score;
    }
}
