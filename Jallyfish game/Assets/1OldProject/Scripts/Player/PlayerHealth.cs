using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerHealth : MonoBehaviour
{
    //щит
    float curArmor; //текущий щит
    float maxArmor = 4; //максимальный щит

    //здоровье
    float curHp; //текущее здоровье
    float maxHp = 10; //максимальное
    public float reastartDelay = 2f;//умирает с паузой


    private void Start()
    {
        curArmor = maxArmor;
        curHp = maxHp;
    }

    public void RecountArmorp(float deltaArmor) //отнимает щит
    {
        // curArmor = 4;
        // deltaArmor = -1;

        float damage_HP = deltaArmor;

        // curArmor = 4;
        // deltaArmor = -1;
        // damage_HP = -1;

        damage_HP += curArmor; 

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

        //PlayerArmorBar.instance.SetValue(curArmor / (float)maxArmor); //отображает в бар


        if (damage_HP < 0)
        {
            print("Щита больше нет");
            RecountHp(damage_HP);
        }
    }

    public void RecountHp(float deltaHp) //Отнимает здоровье
    {
        curHp += deltaHp;
        print("Осталось жизней " + curHp);
        //PlayerHealthBar.instance.SetValue(curHp / (float)maxHp);
        if (curHp <= 0)
        {
            Invoke(nameof(Restart), reastartDelay); 
            print("СМЕРТЬ");
        }
    }

    void Restart() //ìåòîä çàãðóæàåò ñöåíó Game Over
    {
        SceneManager.LoadScene(1);
    }


    //
    public void IncreaseHealth(int level)
    {
        maxHp += (curHp * 0.01f) * ((100f - level) * 0.1f);
        curHp = maxHp;
    }

}
