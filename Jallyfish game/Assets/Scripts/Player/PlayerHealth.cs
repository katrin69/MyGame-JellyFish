using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerHealth : MonoBehaviour
{
    //броня
    public float curArmor; //текущее количество жизней
    int maxArmor = 4; //максимальное количество жизней

    //жизнь
    float curHp; //текущее количество жизней
    int maxHp = 10; //максимальное количество жизней
    public float reastartDelay = 2f;//задержка перезапуска


    private void Start()
    {
        curArmor = maxArmor;
        print("БРОНЯ " + curArmor);

        curHp = maxHp;
        print("ЖИЗНЬ " + curHp);
    }

    public void RecountArmorp(float deltaArmor) //принимает поло и отрец. Метод пересчитывает 
    {
        // curArmor = 4;
        // deltaArmor = -10;

        float damage_HP = deltaArmor;

        // curArmor = 4;
        // deltaArmor = -10;
        // damage_HP = -10;

        damage_HP += curArmor;

        // curArmor = 4;
        // deltaArmor = -10;
        // damage_HP = -6;

        curArmor += deltaArmor;

        // curArmor = -6;
        // deltaArmor = -10;
        // damage_HP = -6;

        if (curArmor < 0)
        {
            curArmor = 0;
        }

        PlayerArmorBar.instance.SetValue(curArmor / (float)maxArmor);

        // curArmor = 0;
        // deltaArmor = -10;
        // damage_HP = -6;

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

}
