using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ELevel //������ ������������ � ��������
{
    Level_1 = 1, //������� ������
    Level_2 = 2, //������� ������
    Level_3 = 3, //������� ������
    Level_4 = 4, //������� ���������
}

public class Meduzi : MonoBehaviour
{
    private ELevel CurrentLevel = ELevel.Level_1; //������� ������� ������
    private ELevel MaxLevel = ELevel.Level_4; //������������ ������� ���������

    private float Health; //��������

    private float BasicHealth = 80; //������� ��������
    private float HealthStep = 20; //���������� �������� ��� ��������� ������

    public void Awake()
    {
        RecalculateHealth(); //��� ����������� ����������� ����� 
    }

    private void RecalculateHealth() //����� ������� ������������� ��������
    {
        //�������� ����� ������� �������� + ��� ������� ���������� �� �������
        Health = BasicHealth + HealthStep * (int)CurrentLevel;
        // 1 - 80 + 20 * 1 = 100
        // 2 - 100 + 20 * 2 = 140
        // 3 - 140 + 20 * 3 = 200
        // 4 - 200 + 20 * 4 = 280

    }

    public void IncreaseLevel()  //����� ��������� ������
    {
        if (CurrentLevel != MaxLevel) //���� ������� ������� �� ����� ������������� ������ ���������
        { //�� ������� ������������� � �������� ���������������
            CurrentLevel++;
            RecalculateHealth();
        }
    }
}
