using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManaBar : MonoBehaviour
{
    private Mana mana;
    private Image barImage;


    private void Awake()
    {
        barImage = transform.Find("bar").GetComponent<Image>();

        mana = new Mana();
    }

    private void Update()
    {
        mana.Update();
        barImage.fillAmount = mana.GetmanaNormalized(); 
    }

}

public class Mana //этот класс маны не зависит от манаБар
{
    public const int MANA_MAX = 100; //константа для общей суммы

    private float manaAmount; //текущее количество маны
    private float manaRegenAmount; //кол-во регенираций

    public Mana() //простой конструктор
    {
        manaAmount = 0;
        manaRegenAmount = 30f;
    }

    public void Update() //обновление маны
    {
        manaAmount += manaRegenAmount * Time.deltaTime; //увеличиваем основную сумму
        manaAmount = Mathf.Clamp(manaAmount, 0f, MANA_MAX); //Значение с плавающей запятой для ограничения в диапазоне, определяемом минимальным и максимальным значениями
    }

    //функция чтобы иметь возможность тратить немного маны
    public void TrySpendMana(int amount)
    {
        if (manaAmount >= amount)
        {
            manaAmount -= amount;
        }
    }

    //функция для возврата нормализованного значения
    public float GetmanaNormalized()
    {
        return manaAmount / MANA_MAX;
    }
}
