using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float transLag = 1f;
    [SerializeField] private float cameraFollowSpeed = 1f;

    [SerializeField] private Vector3 transitionDistance = Vector3.zero;
    [SerializeField] private Transform playerTransform = null;

    private bool hasCamera_Finished_InitialTransition = false;
    private bool hasPlayerTransitioned = false;

    private Vector3 transFinalPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        transFinalPosition = transform.position + transitionDistance;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(hasCamera_Finished_InitialTransition)
        {
            MoveCameraWithPlayer();
        }

        if (hasPlayerTransitioned && !hasCamera_Finished_InitialTransition)
        {
            TrackInitialTransition();
            MoveCamera();
        }
    }
    
    void MoveCamera()
    {
        transform.position = Vector3.Lerp(transform.position, transFinalPosition, transLag);
    }

    private void MoveCameraWithPlayer()
    {
        if(playerTransform.position.y > transform.position.y)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, playerTransform.position.y, transform.position.z) , cameraFollowSpeed);
        }
    }

    private void TrackInitialTransition()
    {
        if (transform.position.y >= playerTransform.position.y - 1f)
            hasCamera_Finished_InitialTransition = true;
    }

    public void SetHasPlayerTransed(bool hpt)
    { hasPlayerTransitioned = hpt; }

    public bool GetHasPlayerTransed()
    {
        return hasPlayerTransitioned;
    }

    public bool GetHasFinishedTransition()
    {
        return hasCamera_Finished_InitialTransition;
    }
}
