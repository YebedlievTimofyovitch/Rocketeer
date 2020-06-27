using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float movementLag = 1f;
    public Transform shipCam = null;
    [SerializeField] private Vector3 distanceFromShip = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SmoothCamFollow();
    }

    void SmoothCamFollow()
    {
        shipCam.position = Vector3.Lerp(shipCam.position,transform.position + distanceFromShip, movementLag *  Time.deltaTime);
        shipCam.LookAt(transform.position, Vector3.up);
    }
}
