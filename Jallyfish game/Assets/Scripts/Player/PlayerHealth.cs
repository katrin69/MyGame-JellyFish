using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerHealth : MonoBehaviour
{
    //�����
    float curArmor; //������� ���������� ������
    float maxArmor = 4; //������������ ���������� ������

    //�����
    float curHp; //������� ���������� ������
    float maxHp = 10; //������������ ���������� ������
    public float reastartDelay = 2f;//�������� �����������


    private void Start()
    {
        curArmor = maxArmor;
        print("��� " + curArmor);

        curHp = maxHp;
        print("����� " + curHp);
    }

    public void RecountArmorp(float deltaArmor) //��������� ���� � �����. ����� ������������� 
    {
        // curArmor = 4;
        // deltaArmor = -1;

        float damage_HP = deltaArmor; //������ ���������� ���� �������� ���� -1

        // curArmor = 4;
        // deltaArmor = -1;
        // damage_HP = -1;

        damage_HP += curArmor; //� ���� ��������� ���������� ����� 

        // curArmor = 4;
        // deltaArmor = -1;
        // damage_HP = 3;

        curArmor += deltaArmor;

        // curArmor = 3;
        // deltaArmor = -1;
        // damage_HP = 3;

        if (curArmor < 0)
        {
            curArmor = 0;
        }

        PlayerArmorBar.instance.SetValue(curArmor / (float)maxArmor);


        print("����� " + curArmor);

        if (damage_HP < 0)
        {
            print("����� ������ ���");
            RecountHp(damage_HP);
        }
    }

    public void RecountHp(float deltaHp) //��������� ���� � �����. ����� ������������� 
    {
        curHp += deltaHp;
        print("����� " + curHp);
        PlayerHealthBar.instance.SetValue(curHp / (float)maxHp);
        if (curHp <= 0)
        {
            Invoke(nameof(Restart), reastartDelay); //�������� �����������
            print("������");
        }
    }

    void Restart() //����� ��������� ����� Game Over
    {
        SceneManager.LoadScene(1);
    }

    //����� ������������� ��������
    public void IncreaseHealth(int level)
    {
        maxHp += (curHp * 0.01f) * ((100f - level) * 0.1f);
        curHp = maxHp;
    }

}
