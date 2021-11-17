using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    public event Action<float> ChangeLevel;

    public int CurrentLevel { get; private set; } = 1; //уровень
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
        ChangeLevel?.Invoke(CurrentLevel); //отображает в бар
    }
    //сохранение
    public void SetNewLevel(int level)
    {
        CurrentLevel = level;
        ChangeLevel?.Invoke(CurrentLevel);
    }
    public void SetNewXp(float level)
    {
        currentXp = level;
    }
    public void SetNewRequiredXp(float level)
    {
        requiredXp = level;
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
        CurrentLevel++;
        currentXp = Mathf.RoundToInt(currentXp - requiredXp); //WTF
        GetComponent<PlayerHealthScript>().IncreaseHealth(CurrentLevel);
        requiredXp = CalculateRequireXp();

        ChangeLevel?.Invoke(CurrentLevel);//отображает в бар

    }

    //метод высчитывает опыт
    private int CalculateRequireXp()
    {
        int solveForRequiredXp = 0;
        for (int levelCycle = 1; levelCycle <= CurrentLevel; levelCycle++)
        {
            solveForRequiredXp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solveForRequiredXp / 4;
    }


    public void GainExperienceScalable(float xpGained, int passedLevel)
    {
        if (passedLevel < CurrentLevel)
        {
            float multiplier = 1 + (CurrentLevel - passedLevel) * 0.01f;
            currentXp += xpGained * multiplier;
        }
        else
        {
            currentXp += xpGained;
        }
    }

}
