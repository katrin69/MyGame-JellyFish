using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelWindow : MonoBehaviour
{
    private Text levelText;
    private Levels levels; //ссылка на скрипт с уровнями

    private void Awake()
    {
        levelText = transform.Find("LevelText").GetComponent<Text>();


    }

    //пишем номер уровня
    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = "" + (levelNumber + 1); //чтобы начинался с одного
    }

    //метод для получения уровней
    public void SetLevelSystem(Levels levels)
    {
        this.levels = levels;

        //когда получаем номер уровня то мы называем уровень
        SetLevelNumber(levels.GetLevel());

        //подписываемся на изменения
        levels.OnLevelChanged += Levels_OnLevelChanged;
    }

    private void Levels_OnLevelChanged(object sender, System.EventArgs e)
    {
        SetLevelNumber(levels.GetLevel());
    }

}
