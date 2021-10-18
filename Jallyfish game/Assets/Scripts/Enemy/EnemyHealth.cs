using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //�� �����
    public float enemyHealth ;
    public float enemyHealthMax = 8f;


    void Start()
    {
        //�� ���������� ������������ ��� ������
        enemyHealth = enemyHealthMax;
    }

    public void DeductHealth(float deductHealth) //�������� �����
    {
        enemyHealth -= deductHealth;
        EnemyHealthBar.instance.SetValue(enemyHealth / (float)enemyHealthMax);

        if (enemyHealth <=0)
        {
            deadEnemy(); //���� ������ 0 �� ��������
        }
       
    }

    void deadEnemy() //�������
    {
        Destroy(gameObject);
    }

}
