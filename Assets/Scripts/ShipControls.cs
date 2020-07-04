using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float thrustStrength = 10f;
    [SerializeField] private float rotationStrength = 10f;
    [SerializeField] private float angleVelDrag = 10f;

    [SerializeField] private float deathForce = 10f;
    private bool hasCrashed = false;
    private bool hasDied = false;
    private bool hasLanded = false;
    private Vector3 deathForceDirection = Vector3.zero;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasCrashed)
        {
            ShipThrust();
            ShipRotaion();
        }
        else if(hasCrashed)
        {
            DeathFX();
        }
    }

    private void ShipThrust()
    {
        rb.AddForce(transform.up * Input.GetAxis("Vertical") * thrustStrength * Time.deltaTime);
    }

    private void ShipRotaion()
    {
        var xAngVel = rb.angularVelocity.x;

        if((!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.D)) && xAngVel != 0f)
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
        xAngVel += Input.GetAxis("Horizontal") * rotationStrength * Time.deltaTime;

        rb.angularVelocity = new Vector3(xAngVel,0f,0f);
    }

    private void DeathFX()
    {
        if (!hasDied)
        {
            DeathPhysics();

            hasDied = true;
        }
    }

    private void DeathPhysics()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.AddForceAtPosition(-deathForceDirection.normalized * deathForce,transform.position);
    }

    private void OnCollisionEnter(Collision obstacle)
    {
        if(obstacle.transform.tag == "Obstacle" && !hasLanded)
        {
            deathForceDirection = obstacle.GetContact(0).point - transform.position;
            hasCrashed = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LandingPad")
        {
            hasLanded = true;
            transform.parent = other.transform;
        }

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
            hasLanded = false;
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
