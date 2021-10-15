using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerHealth : MonoBehaviour
{
    //�����
    int curArmor; //������� ���������� ������
    int maxArmor = 4; //������������ ���������� ������

    //�����
    int curHp; //������� ���������� ������
    int maxHp = 10; //������������ ���������� ������
    public float reastartDelay = 2f;//�������� �����������


    private void Start()
    {
        curArmor = maxArmor;
        print("����� " + curArmor);

        curHp = maxHp;
        print("����� " + curHp);
    }


    public void RecountArmorp(int deltaArmor) //��������� ���� � �����. ����� ������������� 
    {
        curArmor += deltaArmor;
        print("����� " + curArmor);
        PlayerArmorBar.instance.SetValue(curArmor / (float)maxArmor);
        if (curArmor <= 0)
        {
            print("����� ������ ���");
        }
    }

    public void RecountHp(int deltaHp) //��������� ���� � �����. ����� ������������� 
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
