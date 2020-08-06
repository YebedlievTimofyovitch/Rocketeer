﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour
{
    private Rigidbody rb = null;
    private ShipCannon shipCannnon;
    [SerializeField] private float thrustStrength = 10f;
    [SerializeField] private float rotationStrength = 10f;
    [SerializeField] private float angleVelDrag = 10f;

    [SerializeField] private ParticleSystem ThrusterPS = null;
    [SerializeField] private float deathForce = 10f;
    
    public bool hasCrashed = false;
    private Vector3 deathForceDirection = Vector3.zero;

    private bool isRotating = false;
    private bool ThrusterOn = false;
    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasCrashed)
        {
            ShipThrust();
            ShipRotaion(0f);
        }
        else if(hasCrashed)
        {
            DeathFX();
        }
    }

    public void ManageThrusterSmoke(float te)
    {
        var thrusterEmission = ThrusterPS.emission;
        thrusterEmission.rateOverTime = te;

    }

    #region thruster management
    private void ShipThrust()
    {
        if(ThrusterOn)
        rb.AddForce(transform.up * thrustStrength * Time.deltaTime);
    }

    public void TurnThrusterOnOff(bool to)
    {
        ThrusterOn = to;
    }
    #endregion

    #region rotation management
    public void ShipRotaion(float rotationDirection)
    {
        var xAngVel = rb.angularVelocity.x;

        if(!isRotating && xAngVel != 0f)
        {
            if(xAngVel <= 0.1f || xAngVel >= -0.1f)
            {
                xAngVel = 0f;
            }

            if (xAngVel > 0f)
                xAngVel -= angleVelDrag * Time.deltaTime;
            else if (xAngVel < 0f)
            xAngVel += angleVelDrag * Time.deltaTime;
        }
        xAngVel += rotationDirection * rotationStrength * Time.deltaTime;

        rb.angularVelocity = new Vector3(xAngVel,0f,0f);
    }

    public void ShipRotationBool(bool ir)
    {
        isRotating = ir;
    }

    #endregion

    public void DeathFX()
    {
        if (!hasCrashed)
        {
            DeathPhysics();

            

            hasCrashed = true;
        }
    }

    private void DeathPhysics()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        if(deathForceDirection != Vector3.zero)
        rb.AddForceAtPosition(-deathForceDirection.normalized * deathForce,transform.position);
    }

    private void OnCollisionEnter(Collision obstacle)
    {
        if(obstacle.transform.tag == "Obstacle"  || obstacle.transform.tag == "ObstacleExp") //|| obstacle.transform.tag == "ObstacleGold")
        {
            deathForceDirection = obstacle.GetContact(0).point - transform.position;
            hasCrashed = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SceneTransitioner")
        {
            CameraControl mainCam = FindObjectOfType<CameraControl>();
            mainCam.SetHasPlayerTransed(true);
        }

        if(other.tag == "DeathLine")
        {
            hasCrashed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "LandingPad")
        {
            transform.parent = null;
        }

        if (other.tag == "SceneTransitioner")
        {
            foreach(GameObject dLine in GameObject.FindGameObjectsWithTag("DeathLine"))
            {
                if(!dLine.GetComponent<BoxCollider>().enabled)
                dLine.GetComponent<BoxCollider>().enabled = true;
            }


            Destroy(other.gameObject);
        }

        
    }
}
