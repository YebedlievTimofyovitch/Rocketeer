using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnGate
{
    public Vector3 SpawnLocation;
    public bool IsActive;

    public SpawnGate(Vector3 sp , bool ia)
    {
        SpawnLocation = sp;
        IsActive = ia;
    }
}
