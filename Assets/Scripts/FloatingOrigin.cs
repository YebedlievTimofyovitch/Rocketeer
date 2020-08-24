using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FloatingOrigin : MonoBehaviour
{
    [SerializeField]private CameraControl cc = null;
    [SerializeField] private Transform originPoint = null;
    [SerializeField] private Transform shipTransform = null;

    [SerializeField] private float Threshold = 1000.0f;

    private float distanceFromOrigin = 0.0f;

    private float lerpLag = 0.02f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(cc.GetHasPlayerTransed())
        {
            MoveLevelMainObjectsWithPlayer();
        }

        ReturnSceneObjectsToRootPosition();
    }

    private void MoveLevelMainObjectsWithPlayer()
    {
        if(shipTransform.position.y > transform.position.y)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + shipTransform.position.y, transform.position.z) , lerpLag);
        }
    }

    private void ReturnSceneObjectsToRootPosition()
    {
        distanceFromOrigin = Vector3.Distance(transform.position, originPoint.position);

        Vector3 originPosition = originPoint.transform.position;

        if(distanceFromOrigin > Threshold)
        {
            foreach(GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if(go.transform != originPoint)
                go.transform.position -= originPosition;
            }
        }
    }
}
