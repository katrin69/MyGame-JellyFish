  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerHealth : MonoBehaviour
{
    int curHp; //текущее количество жизней
    int maxHp = 10; //максимальное количество жизней
    public float reastartDelay = 4f;//задержка перезапуска

    private void Start()
    {
        curHp = maxHp;
        print(curHp);
    }

    public void RecountHp(int deltaHp) //принимает поло и отрец. ћетод пересчитывает 
    {
        curHp += deltaHp;
        print(curHp);
        PlayerHealthBar.instance.SetValue(curHp / (float)maxHp);
        if (curHp <= 0)
        {
            Invoke(nameof(Restart), reastartDelay); //задержка перезапуска
            print("—ћ≈–“№");
        }
    }

    void Restart() //метод загружает сцены с перезапруском
    { 
        SceneManager.LoadScene(1);
    }
}
