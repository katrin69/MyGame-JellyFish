using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //ХР врага
    public float enemyHealth ;
    public float enemyHealthMax = 8f;
    public float ExperienceToGain = 20f; //опыт
    public EnemyHealthBar healthBar; //бар врага


    void Start()
    {
        //задаём жизнь врага
        enemyHealth = enemyHealthMax;
    }

    public void DeductHealth(float deductHealth,LevelsSystem killrLevelSystem) //передаём урон и систему лэвлов
    {
        enemyHealth -= deductHealth;
        healthBar.SetValue(enemyHealth / (float)enemyHealthMax); 

        if (enemyHealth <=0) //если жизни кончились 
        {
            killrLevelSystem.GainExperienceFlatRate(ExperienceToGain); //передаём опыт в систему лэвлов
            deadEnemy(); //убиваем врага

        }
       
    }

    void deadEnemy() //метод смерти врага
    {
        Destroy(gameObject);
    }

}
