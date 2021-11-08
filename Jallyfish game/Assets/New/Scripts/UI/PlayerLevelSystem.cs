using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    public event Action<float> ChangeLevel;


    public int level; //уровень
    public float currentXp; //текущий опыт
    public float requiredXp; //необходимый опыт


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

    }

    public void GainExperienceFlatRate(float xpGained)//набирание опыта  принимает число которое прибавляется к текущему опыту
    {
        currentXp += xpGained;

        if (currentXp >= requiredXp)
        {
            LevelUp(); //повышает опыт
        }
    }

    public void LevelUp() //метод повышает опыт
    {
        level++;
        currentXp = Mathf.RoundToInt(currentXp - requiredXp); //WTF
        GetComponent<PlayerHealthScript>().IncreaseHealth(level);
        requiredXp = CalculateRequireXp();

        ChangeLevel?.Invoke(level);//отображает в бар

    }

    //метод высчитывает опыт
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
