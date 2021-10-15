using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerHealth : MonoBehaviour
{
    //�����
    int curHp; //������� ���������� ������
    int maxHp = 10; //������������ ���������� ������
    public float reastartDelay = 2f;//�������� �����������


    private void Start()
    {
        curHp = maxHp;
        print("����� " + curHp);
    }


    public void RecountHp(int deltaHp) //��������� ���� � �����. ����� ������������� 
    {
        curHp += deltaHp;

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
