using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelsSystem : MonoBehaviour
{
    public int level; //текущий уровень
    public float currentXp; //опыт который есть в настоящее время
    public float requiredXp; //опыт необходимый для достижения следющего уровня

    private float lerpTimer;
    private float delayTimer;

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

    //функция для получения опыта
    public void GainExperienceFlatRate(float xpGained)
    {
        currentXp += xpGained;
    }

    public void LevelUp()
    {
        level++;
        currentXp = Mathf.RoundToInt(currentXp - requiredXp); //WTF
        GetComponent<PlayerHealth>().IncreaseHealth(level); //увеличиваем здоровьен
    }
}
   

