using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerHealth : MonoBehaviour
{
    //броня
    float curArmor; //текущее количество жизней
    float maxArmor = 4; //максимальное количество жизней

    //жизнь
    float curHp; //текущее количество жизней
    float maxHp = 10; //максимальное количество жизней
    public float reastartDelay = 2f;//задержка перезапуска


    private void Start()
    {
        curArmor = maxArmor;
        print("ЩИТ " + curArmor);

        curHp = maxHp;
        print("ЖИЗНЬ " + curHp);
    }

    public void RecountArmorp(float deltaArmor) //принимает поло и отрец. Метод пересчитывает 
    {
        // curArmor = 4;
        // deltaArmor = -1;

        float damage_HP = deltaArmor; //создаём переменную куда помещаем урон -1

        // curArmor = 4;
        // deltaArmor = -1;
        // damage_HP = -1;

        damage_HP += curArmor; //к этой еременной прибавляем броню 

        // curArmor = 4;
        // deltaArmor = -1;
        // damage_HP = 3;

        curArmor += deltaArmor;

        // curArmor = 3;
        // deltaArmor = -1;
        // damage_HP = 3;

        if (curArmor < 0)
        {
            curArmor = 0;
        }

        PlayerArmorBar.instance.SetValue(curArmor / (float)maxArmor);


        print("БРОНЯ " + curArmor);

        if (damage_HP < 0)
        {
            print("БРОНИ БОЛЬШЕ НЕТ");
            RecountHp(damage_HP);
        }
    }

    public void RecountHp(float deltaHp) //принимает поло и отрец. Метод пересчитывает 
    {
        curHp += deltaHp;
        print("ЖИЗНЬ " + curHp);
        PlayerHealthBar.instance.SetValue(curHp / (float)maxHp);
        if (curHp <= 0)
        {
            Invoke(nameof(Restart), reastartDelay); //задержка перезапуска
            print("СМЕРТЬ");
        }
    }

    void Restart() //метод загружает сцену Game Over
    {
        SceneManager.LoadScene(1);
    }

    //метод увеличивающий здоровье
    public void IncreaseHealth(int level)
    {
        maxHp += (curHp * 0.01f) * ((100f - level) * 0.1f);
        curHp = maxHp;
    }

}
