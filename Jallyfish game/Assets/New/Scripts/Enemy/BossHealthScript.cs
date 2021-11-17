using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthScript : MonoBehaviour
{
    public event Action<BossHealthScript, float> HealthPercentageChanged; 

    //ХР врага
    public float enemyHealth;
    public float enemyHealthMax = 20f;
    public float ExperienceToGain = 20f; //опыт

    protected AudioManager AudioManager;

    void Start()
    {
        //задаём жизнь врага
        enemyHealth = enemyHealthMax;

        HealthPercentageChanged?.Invoke(this, 1);
    }

    public void SetAudioManager(AudioManager audioManager)
    {
        AudioManager = audioManager;
    }

    public void DeductHealth(float deductHealth, PlayerLevelSystem killrLevelSystem) //передаём урон и систему лэвлов
    {
        enemyHealth -= deductHealth;

        HealthPercentageChanged?.Invoke(this, enemyHealth / enemyHealthMax);

        if (enemyHealth <= 0) //если жизни кончились 
        {
            killrLevelSystem.GainExperienceFlatRate(ExperienceToGain); //передаём опыт в систему лэвлов
            deadEnemy(); //убиваем врага
        }
    }

    void deadEnemy() //метод смерти врага
    {
        gameObject.SetActive(false);
        ResourceManager.ReturnToPool(gameObject);
    }
}
