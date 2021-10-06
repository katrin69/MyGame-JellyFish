  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int curHp; //текущее количество жизней
    int maxHp = 10; //максимальное количество жизней

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
        if (curHp <=0)
        {
            print("—ћ≈–“№");
        }
    }
}
