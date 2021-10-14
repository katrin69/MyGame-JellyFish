using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //�� �����
    public float enemyHealth = 2f;
    public float enemyHealthMax = 2f;


    void Start()
    {
        //�� ���������� ������������ ��� ������
        enemyHealth = enemyHealthMax;
    }

    public void DeductHealth(float deductHealth) //�������� �����
    {
        enemyHealth -= deductHealth;
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
