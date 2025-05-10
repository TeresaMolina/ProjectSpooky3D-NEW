using System;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public Transform[] spawnPoints;
    public GameObject playerPrefab;
    public int spawnIndex = 0; //changing later for spawn door

    void Start()
    {
        if (spawnIndex < spawnPoints.Length)
        {
            Instantiate(playerPrefab, 
                spawnPoints[spawnIndex].position, 
                spawnPoints[spawnIndex].rotation);
        }
        else
        {
            Debug.LogWarning("Invalid Spawn");
        }
}

    //private void Instantiate(GameObject playerPrefab, Vector3 position, Quaternion rotation)
    //{
    //    throw new NotImplementedException();
    //}
}
