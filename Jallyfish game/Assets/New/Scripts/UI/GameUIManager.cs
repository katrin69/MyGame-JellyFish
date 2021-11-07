using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    //выбор оружия
    public event Action choosWeaponOne;
    public event Action choosWeaponTwo;
    public event Action choosWeaponThree;
    public event Action choosWeaponFour;

    public event Action OnBackMainMenu;

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

    [Space(10)] //меню
    public Button Continue;
    public Button BackMainMenu;

    public GameObject PauseMenuUI;

    private bool IsPaused = false;

    private void Awake()
    {
        WeaponOne.onClick.AddListener(ChooseOne);
        WeaponTwo.onClick.AddListener(ChooseTwo);
        WeaponThree.onClick.AddListener(ChooseThree);
        WeaponFour.onClick.AddListener(ChooseFour);

        Continue.onClick.AddListener(PauseCheck);
        BackMainMenu.onClick.AddListener(BackMainMenuButton);
    }

    //пауза
    private void Unpause()
    {
        Time.timeScale = 1f;
        IsPaused = false;
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        IsPaused = true;
    }

    private void BackMainMenuButton()
    {
        if (OnBackMainMenu != null)
        {
            Unpause();
            OnBackMainMenu();
        }
    }

    public void PauseCheck()
    {
        if (IsPaused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }

        PauseMenuUI.SetActive(IsPaused);
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

    //пауза

   
}
