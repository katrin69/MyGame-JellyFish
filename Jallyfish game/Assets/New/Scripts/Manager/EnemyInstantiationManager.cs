using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiationManager : MonoBehaviour
{
    private LayerMask TerrainMask;

    private ResourceManager ResourceManager;

    private List<EnemyHealthScript> CurrentEnemies = new List<EnemyHealthScript>();

    private void Awake()
    {
        TerrainMask = LayerMask.GetMask("Terrain");
    }

    public void Init(ResourceManager resourceManager, List<EnemySpawner> enemySpawners)
    {
        ResourceManager = resourceManager;

        foreach (EnemySpawner spawner in enemySpawners)
        {
            for (int i = 0; i < spawner.enemyCount; i++)
            {
                Vector3 spawningPoint = spawner.GetSpawningPoint();
                InstantiateEnemies(EObjectType.Shark, spawningPoint);
            }

            for (int i = 0; i < spawner.enemyCountLittle; i++)
            {
                Vector3 spawningPoint = spawner.GetSpawningPoint();
                InstantiateEnemies(EObjectType.LittleShark, spawningPoint);
            }

            for (int i = 0; i < spawner.enemyCountTerrorist; i++)
            {
                Vector3 spawningPoint = spawner.GetSpawningPoint();
                InstantiateEnemies(EObjectType.TerroristShark, spawningPoint);
            }

            for (int i = 0; i < spawner.enemyCountBoss; i++)
            {
                Vector3 spawningPoint = spawner.GetSpawningPoint();
                //InstantiateEnemies(EObjectType.FirstBoss, spawningPoint);
            }
        }

        foreach (EnemyHealthScript script in CurrentEnemies)
        {
            EnemyMovement enemyMovement = script.gameObject.GetComponent<EnemyMovement>();
            Vector3 patrollinPoint = CurrentEnemies[Random.Range(0, CurrentEnemies.Count)].gameObject.transform.position;
            enemyMovement.SetPatrollingPoint(patrollinPoint);
            Points.Add(script.gameObject.transform.position, patrollinPoint);
        }
    }

    private void InstantiateEnemies(EObjectType enemyType, Vector3 spawnPosition)
    {
            Vector3 pos = new Vector3(spawnPosition.x, 500, spawnPosition.z);
            Ray ray = new Ray(pos, Vector3.down);

            if (Physics.Raycast(ray, out var hitInfo, 1000, TerrainMask))
            {
                pos = hitInfo.point;
                pos.y += 5f;

                GameObject newShark = ResourceManager.GetObjectInstance(enemyType);
                newShark.name = enemyType.ToString();
                newShark.transform.position = pos;
                newShark.SetActive(true);

                EnemyHealthScript enemyHealthScript = newShark.GetComponent<EnemyHealthScript>();
                enemyHealthScript.HealthPercentageChanged += EnemyHealthScript_HealthPercentageChanged;
                CurrentEnemies.Add(enemyHealthScript);
            }
    }

    private void EnemyHealthScript_HealthPercentageChanged(EnemyHealthScript healthScript, float healthPercents)
    {
        //Debug.Log(healthScript.gameObject.name + " : " + healthPercents);

        if (healthPercents <= 0)
        {
            CurrentEnemies.Remove(healthScript);
            healthScript.HealthPercentageChanged -= EnemyHealthScript_HealthPercentageChanged;
        }
    }

    private Dictionary<Vector3, Vector3> Points = new Dictionary<Vector3, Vector3>();

    private void OnDrawGizmos()
    {
        foreach (var p in Points)
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawSphere(p.Key, 0.5f);
            Gizmos.DrawSphere(p.Value, 0.5f);

            Gizmos.color = Color.green;

            Gizmos.DrawLine(p.Key, p.Value);
        }
    }
}
