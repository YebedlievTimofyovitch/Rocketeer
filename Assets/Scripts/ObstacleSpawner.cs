using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacleArray = new GameObject[3] {null,null,null};

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
        yield return new WaitUntil(()=> mainCam.GetHasPlayerTransed());

        while (true)
        {
            bool hasFoundGate = false;

            int gateIndex = 0;

            int spawnPercentage = Random.Range(0, 99);

            GameObject obstacle = null;

            SpawnGate spawnG;

            //DetermineGateState();

            while(!hasFoundGate)
            {
                gateIndex = Random.Range(0, 4);

                spawnG = spawnGates[gateIndex];

                if(spawnG.IsActive)
                {
                    hasFoundGate = true;
                }
            }

            if (spawnPercentage <= 49)
                obstacle = obstacleArray[0];
            else if (spawnPercentage >= 50 && spawnPercentage <= 74)
                obstacle = obstacleArray[1];
            else if (spawnPercentage >= 75)
                obstacle = obstacleArray[2];

            Instantiate(obstacle, GetSpawnLocation(gateIndex)  , Quaternion.identity);

            yield return new WaitForSeconds(1f);
        }
    }

    private Vector3 GetSpawnLocation(int gi)
    {
        return spawnGates[gi].SpawnLocation;
    }

    private void DetermineGateState()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        int a = 0, b = 0, c = 0, d = 0, e = 0;

        foreach(Obstacle obs in obstacles)
        {
            if (obs.transform.position.z == spawnGates[0].SpawnLocation.z)
                a++;
            else if (obs.transform.position.z == spawnGates[1].SpawnLocation.z)
                b++;
            else if (obs.transform.position.z == spawnGates[2].SpawnLocation.z)
                c++;
            else if (obs.transform.position.z == spawnGates[3].SpawnLocation.z)
                d++;
            else if (obs.transform.position.z == spawnGates[4].SpawnLocation.z)
                e++;
        }

        if (a >= 3)
            spawnGates[0].IsActive = false;
        else
            spawnGates[0].IsActive = true;

        if (b >= 3)
            spawnGates[1].IsActive = false;
        else
            spawnGates[1].IsActive = true;

        if (c >= 3)
            spawnGates[2].IsActive = false;
        else
            spawnGates[2].IsActive = true;

        if (c >= 3)
            spawnGates[3].IsActive = false;
        else
            spawnGates[3].IsActive = true;

        if (c >= 3)
            spawnGates[4].IsActive = false;
        else
            spawnGates[4].IsActive = true;


    }
}
