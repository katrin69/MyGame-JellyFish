using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelsSystem : MonoBehaviour
{
    public int level; //������� �������
    public float currentXp; //���� ������� ���� � ��������� �����
    public float requiredXp; //���� ����������� ��� ���������� ��������� ������
    public Text levelText;

    [Header("Multipliers")]
    [Range(1f, 300f)]
    public float additionMultiplier = 300f;
    [Range(2f, 4f)]
    public float powerMultiplier = 2f;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7f;

    private void Start()
    {
        requiredXp = CalculateRequireXp();
        levelText.text = "" + level; //���������� �����

    }

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
        GetComponent<PlayerHealth>().IncreaseHealth(level); //����������� ��������� ����� ��������� �� ����� �������
        requiredXp = CalculateRequireXp();
        levelText.text = "" + level; //���������� �����

    }

    //���������� ���������� �����
    private int CalculateRequireXp()
    {
        int solveForRequiredXp = 0;
        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solveForRequiredXp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solveForRequiredXp / 4;
    }


    public void GainExperienceScalable(float xpGained, int passedLevel)
    {
        if (passedLevel < level)
        {
            float multiplier = 1 + (level - passedLevel) * 0.01f;
            currentXp += xpGained * multiplier;
        }
        else
        {
            currentXp += xpGained;
        }
    }
}


