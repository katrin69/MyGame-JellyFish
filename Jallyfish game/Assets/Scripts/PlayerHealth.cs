using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int curHp; //������� ���������� ������
    int maxHp = 6; //������������ ���������� ������

    private void Start()
    {
        curHp = maxHp;
        print(curHp);
    }

    public void RecountHp(int deltaHp) //��������� ���� � �����. ����� ������������� 
    {
        curHp =+ deltaHp;
        print(curHp);
        if (curHp <=0)
        {
            print("������");
        }
    }
}
