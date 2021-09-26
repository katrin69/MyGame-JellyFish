using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float timeToSpawn = 5f;
    private float timeSinceSpawn;
    private BasicPool objectPool;

    private void Start()
    {
        objectPool = FindObjectOfType<BasicPool>();
    }

    private void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= timeToSpawn)
        {
            GameObject newCritter = objectPool.GetCritter();
            newCritter.transform.position = this.transform.position;
            timeSinceSpawn = 0f;
        }
    }
}
