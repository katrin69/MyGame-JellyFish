using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaBar : MonoBehaviour
{
    public Slider staminaBar;
    public static PlayerStaminaBar instance;

    private float maxStamina = 100f;
    private float currentStamina;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;

    }

    public void UseStamina(float amount)
    {
        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;
        }
        else
        {
            Debug.Log("Щита нет");
        }
    }
}
