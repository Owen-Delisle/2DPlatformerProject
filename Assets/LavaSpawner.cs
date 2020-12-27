using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSpawner : MonoBehaviour
{
    [SerializeField] GameObject lavaPrefab;

    [SerializeField] int lavaSpawnRate;
    Vector3 spawnPosition;

    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnLava();
    }

    private void SpawnLava()
    {
        if (count % lavaSpawnRate == 0)
        {
            Instantiate(lavaPrefab, new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z), Quaternion.identity);
        }
        if (count % (lavaSpawnRate + 1) == 0)
        {
            Instantiate(lavaPrefab, new Vector3(spawnPosition.x + 0.1f, spawnPosition.y, spawnPosition.z), Quaternion.identity);
        }
        count++;
    }

}
