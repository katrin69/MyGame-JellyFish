using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //�� �����
    public float enemyHealth ;
    public float enemyHealthMax = 8f;
    public float ExperienceToGain = 20f; //���� ������� ����� ����������
    public EnemyHealthBar healthBar; //������ ������ � �����


    void Start()
    {
        //�� ���������� ������������ ��� ������
        enemyHealth = enemyHealthMax;
    }

    public void DeductHealth(float deductHealth,LevelsSystem killrLevelSystem) //�������� ����� . ��������� �����
    {
        enemyHealth -= deductHealth;
        healthBar.SetValue(enemyHealth / (float)enemyHealthMax); //������ ��� � ������ ��������� �����

        if (enemyHealth <=0) //���� �������� � ����� ������ 0 �� 
        {
            killrLevelSystem.GainExperienceFlatRate(ExperienceToGain); //����������� ����� ������� �������� ���� � ��������� 20
            deadEnemy(); //���� ������ 0 �� ��������

        }
       
    }

    void deadEnemy() //�������
    {
        Destroy(gameObject);
    }

}
