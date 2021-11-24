using System;
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

    private void Update()
    {
        foreach (var keyValue in CurrentEnemies)
        {
            keyValue.Value.transform.position = CameraManager.WorldToCanvasPosition(keyValue.Key.transform.position);
        }
    }


    //сохранение
    public void FillSaverData(SaverData saverData)
    {
        saverData.EnamyHealth = new List<float>();
        saverData.EnamyType = new List<ESharkType>();
        saverData.EnamyPosition = new List<float[]>();

        foreach (EnemyHealthScript script in CurrentEnemies.Keys)
        {
            saverData.EnamyHealth.Add(script.enemyHealth);
            saverData.EnamyType.Add(script.sharkType);
            saverData.EnamyPosition.Add(new float[] { script.transform.position.x, script.transform.position.y, script.transform.position.z });

        }
    }
    //загрузка
    public void ApplySaverData(SaverData saverData)
    {
        ClearEnemies();

        InstantiateEnemies(saverData);

    }

    //очищение от акул
    public void ClearEnemies()
    {
        foreach (var healthScript in CurrentEnemies.Keys)
        {
            ResourceManager.ReturnToPool(CurrentEnemies[healthScript].gameObject);
            ResourceManager.ReturnToPool(healthScript.gameObject);

            healthScript.HealthPercentageChanged -= EnemyHealthScript_HealthPercentageChanged;
        }
        CurrentEnemies.Clear();
    }

    public void InstantiateEnemies(SaverData saverData)
    {
        for (int i = 0; i < saverData.EnamyType.Count; i++)
        {
            Vector3 positionShark = new Vector3(saverData.EnamyPosition[i][0], saverData.EnamyPosition[i][1], saverData.EnamyPosition[i][2]);
            EnemyHealthScript enemyHealthScript = InstantiateEnemies(saverData.EnamyType[i], positionShark, AudioManager);

            enemyHealthScript.SetNewEnamyHealth(saverData.EnamyHealth[i]);
        }
    }

    public void Init(ResourceManager resourceManager, AudioManager audioManager, CameraManager cameraManager)
    {
        ResourceManager = resourceManager;
        AudioManager = audioManager;
        CameraManager = cameraManager;

        EnemyHealthUIGameObject = ResourceManager.GetObjectInstance(EObjectType.EnemyHealthUI);
        EnemyHealthUIGameObject.SetActive(true);
    }

    public void InstantiateEnemies(List<EnemySpawner> enemySpawners)
    {
        foreach (EnemySpawner spawner in enemySpawners)
        {
            for (int i = 0; i < spawner.enemyCount; i++)
            {
                Vector3 spawningPoint = spawner.GetSpawningPoint();
                InstantiateEnemies(ESharkType.Shark, spawningPoint, AudioManager);
            }

            for (int i = 0; i < spawner.enemyCountLittle; i++)
            {
                Vector3 spawningPoint = spawner.GetSpawningPoint();
                InstantiateEnemies(ESharkType.LittleShark, spawningPoint, AudioManager);
            }

            for (int i = 0; i < spawner.enemyCountTerrorist; i++)
            {
                Vector3 spawningPoint = spawner.GetSpawningPoint();
                InstantiateEnemies(ESharkType.TerroristShark, spawningPoint, AudioManager);
            }

            for (int i = 0; i < spawner.enemyCountBoss; i++)
            {
                Vector3 spawningPoint = spawner.GetSpawningPoint();
                InstantiateEnemies(ESharkType.FirstBoss, spawningPoint, AudioManager);
            }
        }

        List<EnemyHealthScript> enemies = new List<EnemyHealthScript>(CurrentEnemies.Keys);

        foreach (EnemyHealthScript script in enemies)
        {
            EnemyMovement enemyMovement = script.gameObject.GetComponent<EnemyMovement>();
            Vector3 patrollinPoint = enemies[UnityEngine.Random.Range(0, enemies.Count)].gameObject.transform.position;
            enemyMovement.SetPatrollingPoint(patrollinPoint);
            // Points.Add(script.gameObject.transform.position, patrollinPoint);
        }
    }

    private EnemyHealthScript InstantiateEnemies(ESharkType enemyType, Vector3 spawnPosition, AudioManager audioManager)
    {
        Vector3 pos = new Vector3(spawnPosition.x, 500, spawnPosition.z);
        Ray ray = new Ray(pos, Vector3.down);

        EnemyHealthScript enemyHealthScript;

        if (Physics.Raycast(ray, out var hitInfo, 1000, TerrainMask))
        {
            pos = hitInfo.point;
            pos.y += 5f;

        }
        EObjectType objectType = ConvertEnum_SharkToObject(enemyType);

        GameObject newShark = ResourceManager.GetObjectInstance(objectType);

        newShark.name = enemyType.ToString();
        newShark.transform.position = pos;
        newShark.SetActive(true);

        enemyHealthScript = newShark.GetComponent<EnemyHealthScript>();
        enemyHealthScript.HealthPercentageChanged += EnemyHealthScript_HealthPercentageChanged;
        enemyHealthScript.SetAudioManager(audioManager);

        EnemyAttackScript enemyAttackScript = newShark.GetComponent<EnemyAttackScript>();
        enemyAttackScript.SetAudioManager(audioManager);

        GameObject enemyHealthBar = ResourceManager.GetObjectInstance(EObjectType.EnemyHealthBarUI);
        enemyHealthBar.transform.SetParent(EnemyHealthUIGameObject.transform);
        enemyHealthBar.SetActive(true);

        EnamyHealthBar enamyHealthBar = enemyHealthBar.GetComponent<EnamyHealthBar>();

        CurrentEnemies.Add(enemyHealthScript, enamyHealthBar);

        return enemyHealthScript;
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

    private EObjectType ConvertEnum_SharkToObject(ESharkType sharkType)
    {
        switch (sharkType)
        {
            case ESharkType.Shark:
                return EObjectType.Shark;
            case ESharkType.LittleShark:
                return EObjectType.LittleShark;
            case ESharkType.TerroristShark:
                return EObjectType.TerroristShark;
            case ESharkType.FirstBoss:
                return EObjectType.FirstBoss;
            default:
                throw new ArgumentOutOfRangeException("UNKNOWN OBJECT TYPE FOR RESOURCE MANAGER");
        }
    }

}
