using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSpawner : MonoBehaviour
{
    [SerializeField] GameObject lavaUnit;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] int lavaSpawnRate;

    Vector3 spawnPosition;

    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = spawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(count%lavaSpawnRate == 0)
        {
            Instantiate(lavaUnit, new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z), Quaternion.identity);
        }
        if(count%(lavaSpawnRate+1) == 0)
        {
            Instantiate(lavaUnit, new Vector3(spawnPosition.x + 0.1f, spawnPosition.y, spawnPosition.z), Quaternion.identity);
        }
        count++;
    }
}
