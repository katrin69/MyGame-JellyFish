using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaBar : MonoBehaviour
{
    public Image mask;
    float originalSize;

    private void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

    //public Slider staminaBar;

    //private float maxStamina = 100f;
    //private float currentStamina;

    //private void Start()
    //{
    //    currentStamina = maxStamina;
    //    staminaBar.maxValue = maxStamina;
    //    staminaBar.value = maxStamina;
    //}

    //public void SetValue(float amount)
    //{

    //}
}
