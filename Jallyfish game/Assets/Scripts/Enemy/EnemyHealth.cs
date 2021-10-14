using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //’п врага
    public float enemyHealth = 2f;
    public float enemyHealthMax = 2f;


    void Start()
    {
        //’п становитс€ максимальным при старте
        enemyHealth = enemyHealthMax;
    }

    public void DeductHealth(float deductHealth) //¬ычитает жизни
    {
        enemyHealth -= deductHealth;
        if (enemyHealth <=0)
        {
            deadEnemy(); //если жизней 0 то удал€€ем
        }
       
    }

    void deadEnemy() //удал€ем
    {
        Destroy(gameObject);
    }

}
