using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelsSystem : MonoBehaviour
{
    public int level; //������� �������
    public float currentXp; //���� ������� ���� � ��������� �����
    public float requiredXp; //���� ����������� ��� ���������� ��������� ������

    private float lerpTimer;
    private float delayTimer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            GainExperienceFlatRate(20);
        }
        if (currentXp >= requiredXp)
        {
            LevelUp();
        }
    }

    //������� ��� ��������� �����
    public void GainExperienceFlatRate(float xpGained)
    {
        currentXp += xpGained;
    }

    public void LevelUp()
    {
        level++;
        currentXp = Mathf.RoundToInt(currentXp - requiredXp); //WTF
        GetComponent<PlayerHealth>().IncreaseHealth(level); //����������� ���������
    }
}
   

