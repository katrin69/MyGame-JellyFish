using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //’п врага
    public float enemyHealth ;
    public float enemyHealthMax = 8f;
    public float ExperienceToGain = 20f; //опыт который нужно преобрести
    public EnemyHealthBar healthBar; //достаЄм скрипт в баром


    void Start()
    {
        //’п становитс€ максимальным при старте
        enemyHealth = enemyHealthMax;
    }

    public void DeductHealth(float deductHealth,LevelsSystem killrLevelSystem) //¬ычитает жизни . ѕринимает левлы
    {
        enemyHealth -= deductHealth;
        healthBar.SetValue(enemyHealth / (float)enemyHealthMax); //мен€ем его в момент получени€ урона

        if (enemyHealth <=0) //если здоровье у акулы меньше 0 то 
        {
            killrLevelSystem.GainExperienceFlatRate(ExperienceToGain); //срабатывает метод который повышает опыт и принимает 20
            deadEnemy(); //если жизней 0 то удал€€ем

        }
       
    }

    void deadEnemy() //удал€ем
    {
        Destroy(gameObject);
    }

}
