using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiationManager : MonoBehaviour
{
    private LayerMask TerrainMask;

    private ResourceManager ResourceManager;
    private AudioManager AudioManager;
    private CameraManager CameraManager;

    private Dictionary<EnemyHealthScript, EnamyHealthBar> CurrentEnemies = new Dictionary<EnemyHealthScript, EnamyHealthBar>();

    private GameObject EnemyHealthUIGameObject;

    private void Awake()
    {
        TerrainMask = LayerMask.GetMask("Terrain");
    }

    public void Init(ResourceManager resourceManager, AudioManager audioManager, CameraManager cameraManager)
    {
        ResourceManager = resourceManager;
        AudioManager = audioManager;
        CameraManager = cameraManager;

        EnemyHealthUIGameObject = ResourceManager.GetObjectInstance(EObjectType.EnemyHealthUI);
        EnemyHealthUIGameObject.SetActive(true);
    }

    private void Update()
    {
        foreach (var keyValue in CurrentEnemies)
        {
            keyValue.Value.transform.position = CameraManager.WorldToCanvasPosition(keyValue.Key.transform.position);
        }
    }

    public void InstantiateEnemies(List<EnemySpawner> enemySpawners)
    {
        foreach (EnemySpawner spawner in enemySpawners)
        {
            for (int i = 0; i < spawner.enemyCount; i++)
            {
                Vector3 spawningPoint = spawner.GetSpawningPoint();
                InstantiateEnemies(EObjectType.Shark, spawningPoint, AudioManager);
            }

            for (int i = 0; i < spawner.enemyCountLittle; i++)
            {
                Vector3 spawningPoint = spawner.GetSpawningPoint();
                InstantiateEnemies(EObjectType.LittleShark, spawningPoint, AudioManager);
            }

            for (int i = 0; i < spawner.enemyCountTerrorist; i++)
            {
                Vector3 spawningPoint = spawner.GetSpawningPoint();
                InstantiateEnemies(EObjectType.TerroristShark, spawningPoint, AudioManager);
            }

            for (int i = 0; i < spawner.enemyCountBoss; i++)
            {
                Vector3 spawningPoint = spawner.GetSpawningPoint();
                //InstantiateEnemies(EObjectType.FirstBoss, spawningPoint);
            }
        }

        List<EnemyHealthScript> enemies = new List<EnemyHealthScript>(CurrentEnemies.Keys);

        foreach (EnemyHealthScript script in enemies)
        {
            EnemyMovement enemyMovement = script.gameObject.GetComponent<EnemyMovement>();
            Vector3 patrollinPoint = enemies[Random.Range(0, enemies.Count)].gameObject.transform.position;
            enemyMovement.SetPatrollingPoint(patrollinPoint);
           // Points.Add(script.gameObject.transform.position, patrollinPoint);
        }
    }

    private void InstantiateEnemies(EObjectType enemyType, Vector3 spawnPosition, AudioManager audioManager)
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
            enemyHealthScript.SetAudioManager(audioManager);

            EnemyAttackScript enemyAttackScript = newShark.GetComponent<EnemyAttackScript>();
            enemyAttackScript.SetAudioManager(audioManager);

            GameObject enemyHealthBar = ResourceManager.GetObjectInstance(EObjectType.EnemyHealthBarUI);
            enemyHealthBar.transform.SetParent(EnemyHealthUIGameObject.transform);
            enemyHealthBar.SetActive(true);

            EnamyHealthBar enamyHealthBar = enemyHealthBar.GetComponent<EnamyHealthBar>();

            CurrentEnemies.Add(enemyHealthScript, enamyHealthBar);
        }
    }

    private void EnemyHealthScript_HealthPercentageChanged(EnemyHealthScript healthScript, float healthPercents)
    {
        //Debug.Log(healthScript.gameObject.name + " : " + healthPercents);

        if (CurrentEnemies.ContainsKey(healthScript))
        {
            if (healthPercents <= 0)
            {
                ResourceManager.ReturnToPool(CurrentEnemies[healthScript].gameObject);
                CurrentEnemies.Remove(healthScript);
                healthScript.HealthPercentageChanged -= EnemyHealthScript_HealthPercentageChanged;
            }
            else
            {
                CurrentEnemies[healthScript].SetValue(healthPercents);
            }
        }
    }

   // private Dictionary<Vector3, Vector3> Points = new Dictionary<Vector3, Vector3>();

    //private void OnDrawGizmos()
    //{
    //    foreach (var p in Points)
    //    {
    //        Gizmos.color = Color.yellow;

    //        Gizmos.DrawSphere(p.Key, 0.5f);
    //        Gizmos.DrawSphere(p.Value, 0.5f);

    //        Gizmos.color = Color.green;

    //        Gizmos.DrawLine(p.Key, p.Value);
    //    }
    //}
}
