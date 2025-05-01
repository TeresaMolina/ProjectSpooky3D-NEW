using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject monsterPrefab;       // drag your lowres angel prefab here
    public float spawnInterval = 10f;      // seconds between spawns
    public int maxMonsters = 7;            // cap on how many exist at once

    [Header("Area Settings")]
    public Vector3 spawnAreaCenter;        // center of your corridor (e.g. 0,0,0)
    public Vector3 spawnAreaSize = new Vector3(50,0,50);

    private List<GameObject> spawnedMonsters = new List<GameObject>();

    void Start()
    {
        InvokeRepeating(nameof(SpawnMonster), 0f, spawnInterval);
    }

    void SpawnMonster()
    {
        // clean up destroyed ones
        spawnedMonsters.RemoveAll(m => m == null);

        // if we already have enough, bail
        if (spawnedMonsters.Count >= maxMonsters)
            return;

        // pick a random X/Z inside the box
        Vector3 randPos = spawnAreaCenter + new Vector3(
            Random.Range(-spawnAreaSize.x/2, spawnAreaSize.x/2),
            0,
            Random.Range(-spawnAreaSize.z/2, spawnAreaSize.z/2)
        );

        // raycast down onto the floor (layer "Wall")
        if (Physics.Raycast(randPos + Vector3.up*50, Vector3.down, out var hit, 100f, 1<<LayerMask.NameToLayer("Wall")))
            randPos.y = hit.point.y + 1f;

        // spawn
        var monster = Instantiate(monsterPrefab, randPos, Quaternion.Euler(0,Random.Range(0,360),0));
        spawnedMonsters.Add(monster);

        // snap to NavMesh if needed
        var agent = monster.GetComponent<NavMeshAgent>();
        if (agent != null && !agent.isOnNavMesh)
        {
            if (NavMesh.SamplePosition(randPos, out var navHit, 50f, NavMesh.AllAreas))
                agent.Warp(navHit.position);
        }
    }
}
