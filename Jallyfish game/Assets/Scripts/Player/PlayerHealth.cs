using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerHealth : MonoBehaviour
{
    //�����
    public float curArmor; //������� ���������� ������
    int maxArmor = 4; //������������ ���������� ������

    //�����
    float curHp; //������� ���������� ������
    int maxHp = 10; //������������ ���������� ������
    public float reastartDelay = 2f;//�������� �����������


    private void Start()
    {
        curArmor = maxArmor;
        print("����� " + curArmor);

        curHp = maxHp;
        print("����� " + curHp);
    }

    public void RecountArmorp(float deltaArmor) //��������� ���� � �����. ����� ������������� 
    {
        // curArmor = 4;
        // deltaArmor = -10;

        float damage_HP = deltaArmor;

        // curArmor = 4;
        // deltaArmor = -10;
        // damage_HP = -10;

        damage_HP += curArmor;

        // curArmor = 4;
        // deltaArmor = -10;
        // damage_HP = -6;

        curArmor += deltaArmor;

        // curArmor = -6;
        // deltaArmor = -10;
        // damage_HP = -6;

        if (curArmor < 0)
        {
            curArmor = 0;
        }

        PlayerArmorBar.instance.SetValue(curArmor / (float)maxArmor);

        // curArmor = 0;
        // deltaArmor = -10;
        // damage_HP = -6;

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

}
