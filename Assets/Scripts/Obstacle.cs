using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody obstacleRB = null;


    [SerializeField] private float rotationStrength = 50f;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float expRadius = 1f;
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private bool shouldDropNugget = false;
    [SerializeField] private GameObject goldNugget = null;

    [SerializeField] private AudioSource destructSound = null;

    [SerializeField] private GameObject[] childParticleObjects = new GameObject[2] { null, null };

    private void Start()
    {
        obstacleRB = GetComponent<Rigidbody>();

        
        obstacleRB.velocity = (Vector3.down * Random.Range(speed/2,speed));

        RotateObstacle();
    }

    private void Update()
    {

    }

    public float GateZPosition()
    {
        return transform.position.z;
    }

    private void RotateObstacle()
    {
        obstacleRB.AddTorque(Vector3.forward * rotationStrength);
    }

    private void ObstacleExplosion()
    {
        Collider[] objectsInExpRadius = Physics.OverlapSphere(transform.position, expRadius);
        foreach(Collider col in objectsInExpRadius)
        {
            if (col.gameObject.tag == "Player")
            {
                ShipControls shipSC = col.GetComponent<ShipControls>();

                if(shipSC != null)
                shipSC.DeathFX();

                //////////////////////////////////////

                Rigidbody shipRB = col.GetComponent<Rigidbody>();

                if(shipRB != null)
                shipRB.AddExplosionForce(explosionForce,transform.position,expRadius);
            }
            else
            {
                Obstacle colObs = col.GetComponent<Obstacle>();

                if(colObs != null)
                colObs.ObstacleDestruct();
            }
        }
    }

    public void ObstacleDestruct()
    {
        destructSound.Play();

        foreach (GameObject childPS in childParticleObjects)
        {
            if (childPS != null)
            {
                childPS.transform.parent = null;
                ParticleSystem cpoParticleSystem = childPS.GetComponent<ParticleSystem>();
                cpoParticleSystem.Play();
            }
        }

        if (shouldDropNugget && goldNugget != null)
            Instantiate(goldNugget, transform.position , Quaternion.identity);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ObstacleKiller")
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (gameObject.tag == "ObstacleExp")
        {
            ObstacleDestruct();
            ObstacleExplosion();
        }
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Cannon")
        {
            if (gameObject.tag == "ObstacleExp")
            {
                ObstacleDestruct();
                ObstacleExplosion();
            }
            else
            {
                ObstacleDestruct();
            }
        }
    }



}
