using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    //выбор оружия
    public event Action choosWeaponOne;
    public event Action choosWeaponTwo;
    public event Action choosWeaponThree;
    public event Action choosWeaponFour;

    public Button WeaponOne;
    public Button WeaponTwo;
    public Button WeaponThree;
    public Button WeaponFour;

    [Space(10)]
    public Image FillerOne;
    public Image FillerTwo;
    public Image FillerThree;
    public Image FillerFour;

    [Space(10)]
    public PlayerHealthBar PlayerHealthBar;
    public PlayerArmorBar PlayerArmorBar;
    public PlayerStaminaBar PlayerStaminaBar;

    private void Awake()
    {
        WeaponOne.onClick.AddListener(ChooseOne);
        WeaponTwo.onClick.AddListener(ChooseTwo);
        WeaponThree.onClick.AddListener(ChooseThree);
        WeaponFour.onClick.AddListener(ChooseFour);
    }

    private void ChooseOne()
    {
        choosWeaponOne?.Invoke();
    }

    private void ChooseTwo()
    {
        choosWeaponTwo?.Invoke();
    }

    private void ChooseThree()
    {
        choosWeaponThree?.Invoke();
    }

    private void ChooseFour()
    {
        choosWeaponFour?.Invoke();
    }

    public void SetWeaponFiller(EWeapon weapon, float percentage)
    {
        switch (weapon)
        {
            case EWeapon.BulletLight:
                FillerOne.fillAmount = percentage;
                break;
            case EWeapon.BulletJelly:
                FillerTwo.fillAmount = percentage;
                break;
            case EWeapon.JellyHoming:
                FillerThree.fillAmount = percentage;
                break;
            case EWeapon.Fart:
                FillerFour.fillAmount = percentage;
                break;
        }
    }

    public void ChangeHealthe(float curHp)
    {
        PlayerHealthBar.SetValue(curHp);
    }

    public void ChangeArmor(float curArmor)
    {
        PlayerArmorBar.SetValue(curArmor);
    }

    public void ChangeStamina(float curStam)
    {
        PlayerStaminaBar.SetValue(curStam);
    }
}
