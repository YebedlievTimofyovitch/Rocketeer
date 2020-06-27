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
    private Vector3 contactNormal = Vector3.zero;

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
            rb.useGravity = false;

            rb.velocity = Vector3.zero;
            rb.AddForce(contactNormal * deathForce);

            hasDied = true;
        }
    }

    private void OnCollisionEnter(Collision obstacle)
    {
        if(obstacle.transform.tag == "Obstacle")
        {
            contactNormal = obstacle.GetContact(0).normal;
            hasCrashed = true;
        }
    }
}
