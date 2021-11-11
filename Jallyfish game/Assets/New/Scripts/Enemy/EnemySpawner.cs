using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float enemyCount = 5f;
    public float enemyCountLittle = 4f;
    public float enemyCountTerrorist = 3f; 
    public float enemyCountBoss = 0f;

    float xPos;
    float zPos;

    private Collider SpawnerCollider;

    private void Awake()
    {
        SpawnerCollider = GetComponent<Collider>();
    }

    public Vector3 GetSpawningPoint()
    {
         xPos = Random.Range(SpawnerCollider.bounds.min.x, SpawnerCollider.bounds.max.x);
         zPos = Random.Range(SpawnerCollider.bounds.min.z, SpawnerCollider.bounds.max.z);

        return new Vector3(xPos, 0, zPos);
    }
}
