  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    int curHp; //������� ���������� ������
    int maxHp = 10; //������������ ���������� ������

    private void Start()
    {
        curHp = maxHp;
        print(curHp);
    }

    public void RecountHp(int deltaHp) //��������� ���� � �����. ����� ������������� 
    {
        curHp += deltaHp;
        print(curHp);
        PlayerHealthBar.instance.SetValue(curHp / (float)maxHp);
        if (curHp <= 0)
        {

            print("������");
        }
    }

}
