using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManaBar : MonoBehaviour
{
    private Mana mana;
    private Image barImage;


    private void Awake()
    {
        barImage = transform.Find("bar").GetComponent<Image>();

        mana = new Mana();
    }

    private void Update()
    {
        mana.Update();
        barImage.fillAmount = mana.GetmanaNormalized(); 
    }

}

public class Mana //���� ����� ���� �� ������� �� �������
{
    public const int MANA_MAX = 100; //��������� ��� ����� �����

    private float manaAmount; //������� ���������� ����
    private float manaRegenAmount; //���-�� �����������

    public Mana() //������� �����������
    {
        manaAmount = 0;
        manaRegenAmount = 30f;
    }

    public void Update() //���������� ����
    {
        manaAmount += manaRegenAmount * Time.deltaTime; //����������� �������� �����
        manaAmount = Mathf.Clamp(manaAmount, 0f, MANA_MAX); //�������� � ��������� ������� ��� ����������� � ���������, ������������ ����������� � ������������ ����������
    }

    //������� ����� ����� ����������� ������� ������� ����
    public void TrySpendMana(int amount)
    {
        if (manaAmount >= amount)
        {
            manaAmount -= amount;
        }
    }

    //������� ��� �������� ���������������� ��������
    public float GetmanaNormalized()
    {
        return manaAmount / MANA_MAX;
    }
}
