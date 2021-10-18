using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels
{
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
        }
    }
}
   

