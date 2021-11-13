﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    public event Action<EnemyHealthScript, float> HealthPercentageChanged;

    //ХР врага
    public float enemyHealth;
    public float enemyHealthMax = 8f;
    public float ExperienceToGain = 20f; //опыт


    Animator animator;

    void Start()
    {
        //задаём жизнь врага
        enemyHealth = enemyHealthMax;
        HealthPercentageChanged?.Invoke(this, 1);

        animator = GetComponent<Animator>(); //ищем на акуле  

    }

    public void DeductHealth(float deductHealth, PlayerLevelSystem killrLevelSystem) //передаём урон и систему лэвлов
    {
        enemyHealth -= deductHealth;

        HealthPercentageChanged?.Invoke(this, enemyHealth / enemyHealthMax); //отображает жизни 

        if (enemyHealth <= 0) //если жизни кончились 
        {
            killrLevelSystem.GainExperienceFlatRate(ExperienceToGain); //передаём опыт в систему лэвлов
            //deadEnemy(); //убиваем врага
            StartCoroutine(DeadEnemy());
        }
    }

    //void deadEnemy() //метод смерти врага
    //{

    //    FindObjectOfType<AudioManager>().Play("SoundEnemyDead");

    //    animator.SetBool("Dead", true);

    //    gameObject.SetActive(false);
    //    ResourceManager.ReturnToPool(gameObject);
    //}

    private IEnumerator DeadEnemy()
    {
        FindObjectOfType<AudioManager>().Play("SoundEnemyDead");
        animator.SetBool("Dead", true); //анимация смерти
        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
        ResourceManager.ReturnToPool(gameObject);
    }
}
