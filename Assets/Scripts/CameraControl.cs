using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float transLag = 1f;
    [SerializeField] private Vector3 transitionDistance = Vector3.zero;
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
        if(hasPlayerTransitioned)
        MoveCamera();
    }
    
    void MoveCamera()
    {
        transform.position = Vector3.Lerp(transform.position, transFinalPosition, transLag);
        if(transform.position == (transFinalPosition))
        Destroy(gameObject.GetComponent<CameraControl>());
    }

    public void SetHasPlayerTransed(bool hpt)
    { hasPlayerTransitioned = hpt; }

    public bool GetHasPlayerTransed()
    {
        return hasPlayerTransitioned;
    }
}
