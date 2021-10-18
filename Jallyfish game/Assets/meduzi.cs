using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ELevel //создаЄм перечислени€ с уровн€ми
{
    Level_1 = 1, //уровень ѕервый
    Level_2 = 2, //уровень ¬торой
    Level_3 = 3, //уровень “ретий
    Level_4 = 4, //уровень четвертый
}

public class Meduzi : MonoBehaviour
{
    private ELevel CurrentLevel = ELevel.Level_1; //текущий уровень ѕервый
    private ELevel MaxLevel = ELevel.Level_4; //ћаксимальный уровень „етвертый

    private float Health; //здоровье

    private float BasicHealth = 80; //Ѕазовое здоровье
    private float HealthStep = 20; //увеличение здоровь€ при повышении уровн€

    public void Awake()
    {
        RecalculateHealth(); //при пробуждении срабатывает метод 
    }

    private void RecalculateHealth() //метод который пересчитывает здоровье
    {
        //«доровье равно базовое здоровье + шаг здорвь€ умноженный на уровень
        Health = BasicHealth + HealthStep * (int)CurrentLevel;
        // 1 - 80 + 20 * 1 = 100
        // 2 - 100 + 20 * 2 = 140
        // 3 - 140 + 20 * 3 = 200
        // 4 - 200 + 20 * 4 = 280

    }

    public void IncreaseLevel()  //метод повышени€ уровн€
    {
        if (CurrentLevel != MaxLevel) //если текущий уровень не равен максимальному уровню четвЄртому
        { //то уровень увеличиваетс€ и здоровье пересчитываетс€
            CurrentLevel++;
            RecalculateHealth();
        }
    }
}
