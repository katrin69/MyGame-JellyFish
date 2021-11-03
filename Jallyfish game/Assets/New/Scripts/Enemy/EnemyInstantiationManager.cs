using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiationManager : MonoBehaviour
{
    public float enemyCount = 10f;
    public float enemyCountLittle = 8f;
    public float enemyCountTerrorist = 5f;


    private LayerMask TerrainMask;

    private ResourceManager ResourceManager;

    private Transform SpawningFreeTarget;

    private Vector4 SpawningZone;

    private List<EnemyHealthScript> CurrentEnemies = new List<EnemyHealthScript>();

    private void Awake()
    {
        TerrainMask = LayerMask.GetMask("Terrain");
    }

    public void Init(ResourceManager resourceManager, Transform spawningFreeTarget, Vector4 rect)
    {
        ResourceManager = resourceManager;
        SpawningFreeTarget = spawningFreeTarget;
        SpawningZone = rect;

        InstantiateEnemies(EObjectType.Shark, enemyCount);
        InstantiateEnemies(EObjectType.LittleShark, enemyCountLittle);
        InstantiateEnemies(EObjectType.TerroristShark, enemyCountTerrorist);

    }

    private void InstantiateEnemies(EObjectType enemyType, float enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 freeCenter = SpawningFreeTarget.position;

            int upDown = Random.Range(0, 2);

            float xPos;

            if (upDown == 0)
            {
                xPos = Random.Range(freeCenter.x + 15f, SpawningZone.w);
            }
            else
            {
                xPos = Random.Range(SpawningZone.z, freeCenter.x - 15f);
            }

            int leftRight = Random.Range(0, 2);

            float zPos;

            if (leftRight == 0)
            {
                zPos = Random.Range(SpawningZone.x, freeCenter.z - 15f);
            }
            else
            {
                zPos = Random.Range(freeCenter.z + 15f, SpawningZone.y);
            }

            Vector3 pos = new Vector3(xPos, 500, zPos);
            Ray ray = new Ray(pos, Vector3.down);

            if (Physics.Raycast(ray, out var hitInfo, 1000, TerrainMask))
            {
                pos = hitInfo.point;
                pos.y += 5f;

                GameObject newShark = ResourceManager.GetObjectInstance(enemyType);
                newShark.name = "Shark_" + i;
                newShark.transform.position = pos;
                newShark.SetActive(true);

                EnemyHealthScript enemyHealthScript = newShark.GetComponent<EnemyHealthScript>();
                enemyHealthScript.HealthPercentageChanged += EnemyHealthScript_HealthPercentageChanged;
                CurrentEnemies.Add(enemyHealthScript);
            }
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
}
