using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels
{
    private int level; //������� �������
    private int experience; //���� ������� ���� � ��������� �����
    private int experienceToNextLevel; //���� ����������� ��� ���������� ��������� ������

    //������ �����������
    public Levels()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 100; //����� ������� 100 ����� ����� ��� ��������� �������
    }

    //����� ��� ���������� �����
    public void AddExperience(int amount)
    {
        //����������� ���� �� �����
        experience += amount;
        //���������� ��������� �� ���� ����������� �����
        if (experience >= experienceToNextLevel)
        {
            level++;
            //����� ���������� ����
            experience -= experienceToNextLevel;
        }
    }
}
   

