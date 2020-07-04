using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody obstacleRB = null;

    [SerializeField]private float speed = 50f;

    private void Start()
    {
        obstacleRB = GetComponent<Rigidbody>();
        obstacleRB.velocity = (Vector3.down * Time.deltaTime * speed);
    }

    private void FixedUpdate()
    {

    }

    public float GateZPosition()
    {
        return transform.position.z;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ObstacleKiller")
            Destroy(gameObject);
    }


}
