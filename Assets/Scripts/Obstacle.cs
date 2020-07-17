using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody obstacleRB = null;

    [SerializeField] private float rotationStrength = 50f;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float ExpRadius = 1f;

    [SerializeField] private GameObject childParticleObject = null;

    private void Start()
    {
        obstacleRB = GetComponent<Rigidbody>();

        speed = Random.Range(1f,2f);
        obstacleRB.velocity = (Vector3.down *  speed);

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
        Collider[] objectsInExpRadius = Physics.OverlapSphere(transform.position, ExpRadius);
        foreach(Collider col in objectsInExpRadius)
        {
            if (col.GetComponent<Obstacle>() == null)
            {
                if (col.gameObject.tag == "Player")
                    print("Destroyed Player");
            }
            else
            {
                Obstacle colObs = col.GetComponent<Obstacle>();
                colObs.ObsDestruction(col.gameObject);
            }
        }
    }

    public void ObsDestruction(GameObject g)
    {
        MeshRenderer obsMesh = g.GetComponent<MeshRenderer>();
        obsMesh.material.color = Color.white;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ObstacleKiller")
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(gameObject.tag == "ObstacleExp")
        {
            ParticleSystem[] explosionVFXchildren = GetComponentsInChildren<ParticleSystem>();
            foreach(ParticleSystem childPS in explosionVFXchildren)
            {
                childPS.transform.parent = null;
                Destroy(gameObject);
                ParticleSystem cpoParticleSystem = childPS.GetComponent<ParticleSystem>();
                cpoParticleSystem.Play();
                var particleSystemMain = cpoParticleSystem.main;
                particleSystemMain.stopAction = ParticleSystemStopAction.Destroy;
            }
            Destroy(gameObject);
        }
        else
        {
            childParticleObject.transform.parent = null;
            Destroy(gameObject);
            ParticleSystem cpoParticleSystem = childParticleObject.GetComponent<ParticleSystem>();
            cpoParticleSystem.Play();
            var particleSystemMain = cpoParticleSystem.main;
            particleSystemMain.stopAction = ParticleSystemStopAction.Destroy;
        }
    }

    


}
