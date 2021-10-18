using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //’п врага
    public float enemyHealth ;
    public float enemyHealthMax = 8f;


    void Start()
    {
        //’п становитс€ максимальным при старте
        enemyHealth = enemyHealthMax;
    }

    public void DeductHealth(float deductHealth) //¬ычитает жизни
    {
        enemyHealth -= deductHealth;
        EnemyHealthBar.instance.SetValue(enemyHealth / (float)enemyHealthMax);

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
