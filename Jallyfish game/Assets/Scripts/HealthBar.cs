using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
   
    public bool showBar; //��� ������ � ������� ����
    public float barWidth;  //������ ����
    public float barHeight; //������ ����

    //��, ������� ����� ������������ � ����
    public int health;
    public int healthMax;

    void Start()
    {
        showBar = false;//�������� ��� ��� ������
    }

    void OnGUI()
    {
       if (showBar) //���� ��� ������������
        {
            //������ ������, ������� ����� ������������ � 2 ���������
            string str;
            if (health > 0) { str = health + " / " + healthMax; }
            else { str = "Dead"; }
            //������ ���
            GUI.Box(
                    new Rect(Screen.width / 2 - barWidth / 2, barHeight, barWidth, barHeight),
                    str);
        }
    }
}
