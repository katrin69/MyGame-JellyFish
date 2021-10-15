using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerHealth : MonoBehaviour
{
    //жизнь
    int curHp; //текущее количество жизней
    int maxHp = 10; //максимальное количество жизней
    public float reastartDelay = 2f;//задержка перезапуска


    private void Start()
    {
        curHp = maxHp;
        print("∆»«Ќ№ " + curHp);
    }


    public void RecountHp(int deltaHp) //принимает поло и отрец. ћетод пересчитывает 
    {
        curHp += deltaHp;

        PlayerHealthBar.instance.SetValue(curHp / (float)maxHp);
        if (curHp <= 0)
        {
            Invoke(nameof(Restart), reastartDelay); //задержка перезапуска
            print("—ћ≈–“№");
        }
    }

    void Restart() //метод загружает сцену Game Over
    {
        SceneManager.LoadScene(1);
    }

}
