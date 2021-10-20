using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelsSystem : MonoBehaviour
{
    public int level; //текущий уровень
    public float currentXp; //опыт который есть в настоящее время
    public float requiredXp; //опыт необходимый для достижения следющего уровня
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
        levelText.text = "" + level; //отображает левел
        Load();


    }

    public void GainExperienceFlatRate(float xpGained)//метод для получения опыта
    {
        currentXp += xpGained;
        Save();

        if (currentXp >= requiredXp)
        {
            LevelUp(); // повышает опыт
        }
    }

    public void LevelUp()
    {
        level++;
        currentXp = Mathf.RoundToInt(currentXp - requiredXp); //WTF
        GetComponent<PlayerHealth>().IncreaseHealth(level); //увеличиваем здоровьен когда переходит на новый уровень
        requiredXp = CalculateRequireXp();
        levelText.text = "" + level; //отображает левел

    }

    //вычисление требуемого опыта
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



    public void Save() //метод сохранения сохраняет переменные
    {
        PlayerPrefs.SetFloat("currentXp", currentXp);

        PlayerPrefs.Save(); //сохраняет данные на диск
    }

    public void Load() //метод загрузки делает проверку
    {
        if (PlayerPrefs.HasKey("currentXp")) //существует ли ключ с таким именем
        {
            currentXp = PlayerPrefs.GetFloat("currentXp"); //если да то переменная получает сохранённое 
        }


    }
}


