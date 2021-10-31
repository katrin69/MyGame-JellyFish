using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelsSystem : MonoBehaviour
{
    public int level; //уровень
    public float currentXp; //текущий опыт
    public float requiredXp; //необходимый опыт
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

        if (levelText != null)
        {
            levelText.text = "" + level;
        }
        
        Load();
    }

    public void GainExperienceFlatRate(float xpGained)//набирание опыта  принимает число которое прибавляется к текущему опыту
    {
        currentXp += xpGained;
        Save(); 

        if (currentXp >= requiredXp)
        {
            LevelUp(); //повышает опыт
        }
    }

    public void LevelUp() //метод повышает опыт
    {
        level++;
        currentXp = Mathf.RoundToInt(currentXp - requiredXp); //WTF
        GetComponent<PlayerHealth>().IncreaseHealth(level); 
        requiredXp = CalculateRequireXp();

        if (levelText != null)
        {
            levelText.text = "" + level;
        }

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


    //сохранение не очень
    public void Save() 
    {
        PlayerPrefs.SetFloat("currentXp", currentXp);

        PlayerPrefs.Save(); 
    }

    public void Load() 
    {
        if (PlayerPrefs.HasKey("currentXp")) 
        {
            currentXp = PlayerPrefs.GetFloat("currentXp"); 
        }
    }
}


