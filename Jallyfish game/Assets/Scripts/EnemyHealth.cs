using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //�� �����
    public int health;
    public int healthMax;

    void Start()
    {
        //�� ���������� ������������ ��� ������
        health = healthMax;
    }
}
