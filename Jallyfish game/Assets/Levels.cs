using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels
{
   // public event EventHandler OnExperienceChanged; //событие для изменения опыта
    public event EventHandler OnLevelChanged; //событие для изменения уровня


    private int level; //текущий уровень
    private int experience; //опыт который есть в настоящее время
    private int experienceToNextLevel; //опыт необходимый для достижения следющего уровня

    //делаем конструктор
    public Levels()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 100; //нужно набрать 100 опыта чтобы был следующий уровень
    }


    //метод для добавления опыта
    public void AddExperience(int amount)
    {
        //увеличивает опыт на сумму
        experience += amount;
        //прорверяет превышает ли опыт необходимую сумму
        if (experience >= experienceToNextLevel)
        {
            level++;
            //затем сбрасываем опыт
            experience -= experienceToNextLevel;
            
            if (OnLevelChanged != null) //вызываем изменение уровня
            {
                OnLevelChanged(this, EventArgs.Empty); //в видео мужик говорил что это просто но нет
            }
        }
        //if (OnExperienceChanged != null) //проверяем подписчиков на это событие. Если есть то делаем какуето хрень
        //{
        //    OnExperienceChanged(this, EventArgs.Empty);
        //}
    }


    public int GetLevel() {
        return level;
    }
}
   

