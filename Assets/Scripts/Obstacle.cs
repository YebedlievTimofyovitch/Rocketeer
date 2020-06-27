using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody obstacleRB = null;

    [SerializeField]private float speedMinRange = 1f;
    [SerializeField]private float speedMaxRange = 3f;

    private void Start()
    {
        obstacleRB = GetComponent<Rigidbody>();
        obstacleRB.velocity = (Vector3.down * Time.deltaTime * Random.Range(speedMinRange,speedMaxRange));
    }

    private void FixedUpdate()
    {
        
    }
}
