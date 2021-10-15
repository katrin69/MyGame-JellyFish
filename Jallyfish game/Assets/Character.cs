using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour //����� ������� �������� ��� ���� ������������
{
    public enum BodyArmor //������ ��� �����������
    {
        None,
        Tier_1,
        Tier_2,
        Tier_3
    }

    private BodyArmor bodyArmor; //���� ��� �������� ������

    //������ �����������
    public Character()
    {
        bodyArmor = BodyArmor.None; //��������� ����� �� ���������
    }

    public BodyArmor GetEquippedBodyArmor() //������� �������
    {
        return bodyArmor;
    }
    public void SetEquippedBodyArmor(BodyArmor bodyArmor) //��������� ������
    {
        this.bodyArmor = bodyArmor;
    }
}
 