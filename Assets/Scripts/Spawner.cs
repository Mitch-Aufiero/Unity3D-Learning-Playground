using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    [Tooltip("Object to Spawn")]
    public GameObject Spawnable;

    [Tooltip("Time in seconds between each spawn")]
    public float frequency;

    [Tooltip("Stops spawning objects after this number of spawns (-1 for infinite)")]
    public int maxSpawns = 3;

    //[Tooltip("Limits the amount of active objects in the scene")]
    //public int maxActiveSpawns = 3;


    int totalSpawnCount;
    Guid spawnableGuid;

    // Start is called before the first frame update
    void Start()
    {
        spawnableGuid = Guid.NewGuid();
        totalSpawnCount = 0;
        InvokeRepeating(nameof(Spawn), 2.0f, frequency);
    }

    
    void Spawn()
    {
        if (totalSpawnCount >= maxSpawns)
            Destroy(this);

        if(totalSpawnCount< maxSpawns || maxSpawns == -1)
        {
            totalSpawnCount++;
            GameObject spawnable = Instantiate(Spawnable, new Vector3(0, 0, 0), Quaternion.identity);
            spawnable.transform.localPosition = transform.position;

            spawnable.transform.SetParent(transform);
            spawnable.name += spawnableGuid;
        }
        
    }
}
