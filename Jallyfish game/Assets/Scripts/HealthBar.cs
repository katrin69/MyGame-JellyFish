using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
   
    public bool showBar; //Для показа и скрытия бара
    public float barWidth;  //Ширина бара
    public float barHeight; //Высота бара

    //Хп, которое будет отображаться в баре
    public int health;
    public int healthMax;

    void Start()
    {
        showBar = false;//Скрываем бар при старте
    }

    void OnGUI()
    {
       if (showBar) //Если бар показывается
        {
            //Создаём строку, которая будет отображаться в 2 вариантах
            string str;
            if (health > 0) { str = health + " / " + healthMax; }
            else { str = "Dead"; }
            //Рисуем бар
            GUI.Box(
                    new Rect(Screen.width / 2 - barWidth / 2, barHeight, barWidth, barHeight),
                    str);
        }
    }
}
