using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacleArray = new GameObject[3] { null, null, null };

    [SerializeField]
    private SpawnGate[] spawnGates = new SpawnGate[5]
    {
        new SpawnGate(Vector3.zero,true),
        new SpawnGate(Vector3.zero,true),
        new SpawnGate(Vector3.zero,true),
        new SpawnGate(Vector3.zero,true),
        new SpawnGate(Vector3.zero,true)
    };

    private CameraControl mainCam = null;

    private void Start()
    {
        mainCam = FindObjectOfType<CameraControl>();

        StartCoroutine(SpawnObstacles());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnObstacles()
    {
        yield return new WaitUntil(() => mainCam.GetHasPlayerTransed());


        while (true)
        {
            foreach (SpawnGate gate in spawnGates)
            {
                gate.IsActive = GetRandomBool();
            }

            foreach (SpawnGate gate in spawnGates)
            {

                if (gate.IsActive == true)
                { 
                    int spawnPercentage = Random.Range(0, 99);

                    GameObject obstacle = null;

                    if (spawnPercentage <= 49)
                        obstacle = obstacleArray[0];
                    else if (spawnPercentage >= 50 && spawnPercentage <= 74)
                        obstacle = obstacleArray[1];
                    else if (spawnPercentage >= 75)
                        obstacle = obstacleArray[2];

                    Instantiate(obstacle, new Vector3(gate.SpawnLocation.x , transform.position.y, gate.SpawnLocation.z) , obstacle.transform.rotation);
                }
            }

            yield return new WaitForSeconds(3f);
        }
    }

    private bool GetRandomBool()
    {
        float rand = Random.Range(0.0f, 1.0f);
        if (rand  >= 0.5)
            return true;
        else
            return false;
    }
    
}
