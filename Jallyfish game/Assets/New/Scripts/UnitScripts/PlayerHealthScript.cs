using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealthScript : MonoBehaviour
{
    public event Action<float> ChangeHealth;
    public event Action<float> ChangeArmor;

    public event Action PlayerDead;

    //щит
    public float curArmor { get; private set; } //текущий щит
    float maxArmor = 4; //максимальный щит

    //здоровье
    public float CurrentHP { get; private set; }//текущее здоровье
    float maxHp = 10; //максимальное
    public float reastartDelay = 2f;//умирает с паузой

    public Animator animator;

    private void Start()
    {
        curArmor = maxArmor;
        CurrentHP = maxHp;
    }

    public void SetNewHealth(float value)
    {
        CurrentHP = value;
        ChangeHealth?.Invoke(CurrentHP / maxHp);
    }

    public void SetNewArmor(float value)
    {
        curArmor = value;
        ChangeArmor?.Invoke(curArmor / maxArmor);
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

        ChangeArmor?.Invoke(curArmor / maxArmor);//отображает в бар

        if (damage_HP < 0)
        {
            print("Щита больше нет");
            //RecountHp(damage_HP);
            StartCoroutine(RecountHp(damage_HP));
        }
    }

    //public void RecountHp(float deltaHp) //Отнимает здоровье
    //{
    //    curHp += deltaHp;
    //    print("Осталось жизней " + curHp);

    //    ChangeHealth?.Invoke(curHp / maxHp);//отображает в бар

    //    if (curHp <= 0)
    //    {
    //        animator.SetBool("PlayerDead", true);
    //        //я должна включить сцену Конец Игры через 2 секунды после смерти, чтобы проигралась анимация смерти бро
    //        //Invoke(nameof(Restart), reastartDelay);
    //        print("СМЕРТЬ");
    //    }
    //}

    private IEnumerator RecountHp(float deltaHp)
    {
        CurrentHP += deltaHp;
        print("Осталось жизней " + CurrentHP);

        ChangeHealth?.Invoke(CurrentHP / maxHp);//отображает в бар

        if (CurrentHP <= 0)
        {
            animator.SetBool("PlayerDead", true);
            yield return new WaitForSeconds(reastartDelay);

            PlayerDead?.Invoke();
            print("СМЕРТЬ");
        }     
    }

    public void IncreaseHealth(int level)
    {
        maxHp += (CurrentHP * 0.01f) * ((100f - level) * 0.1f);
        CurrentHP = maxHp;
    }
}
