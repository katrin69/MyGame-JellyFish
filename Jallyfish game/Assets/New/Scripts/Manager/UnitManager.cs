using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    //будет отвечать на связь твоего перса со всеми внутренними системами 

    public event Action<EWeapon, float> WeaponColldownChanged;

    public event Action<float> ChangeHealth; 
    public event Action<float> ChangeArmor;
    public event Action<float> ChangeFast;

    private PlayerHealthScript PlayerHealthScript; //скрипт со здоровьем
    private PlayerMovement PlayerMovementScript; //скрипт со скоростью 
    private GroundChecker GroundChecker;//скрипт с лучём
    private WeaponManager WeaponManager; //скрипит с видами оружия

    private void Awake()
    {
        PlayerMovementScript = GetComponent<PlayerMovement>();
        GroundChecker = GetComponent<GroundChecker>();
        WeaponManager = GetComponent<WeaponManager>();
        PlayerHealthScript = GetComponent<PlayerHealthScript>();

        WeaponManager.WeaponColldownChanged += WeaponManager_WeaponColldownChanged;
        PlayerHealthScript.ChangeHealth += PlayerHealthScript_ChangeHealth;
        PlayerHealthScript.ChangeArmor += PlayerHealthScript_ChangeArmor;
        PlayerMovementScript.ChangeFast += PlayerMovementScript_ChangeFast;
    }

    private void PlayerMovementScript_ChangeFast(float curStam)
    {
        ChangeFast?.Invoke(curStam);
    }

    private void PlayerHealthScript_ChangeArmor(float curArm)
    {
        ChangeArmor?.Invoke(curArm);
    }

    private void PlayerHealthScript_ChangeHealth(float curHP)
    {
        ChangeHealth?.Invoke(curHP);
    }

    private void WeaponManager_WeaponColldownChanged(EWeapon weapon, float cooldownPercent)
    {
        WeaponColldownChanged?.Invoke(weapon, cooldownPercent);
    }

    private void Update()
    {
        float difference = GroundChecker.CheckGround();
        PlayerMovementScript.SetVerticalPosition(difference);
    }

    public void Init(ResourceManager resourceManager)
    {
        PlayerLevelSystem levelsSystem = GetComponent<PlayerLevelSystem>();
        WeaponManager.Init(resourceManager, levelsSystem);
    }

    //методы для выбора оружия
    public void ChoosWeaponOne()
    {
        WeaponManager.ChoosWeaponOne();
    }

    public void ChoosWeaponTwo()
    {
        WeaponManager.ChoosWeaponTwo();
    }

    public void ChoosWeaponThree()
    {
        WeaponManager.ChoosWeaponThree();
    }

    public void ChoosWeaponFour()
    {
        WeaponManager.ChoosWeaponFour();
    }

    public void Shoot()
    {
        WeaponManager.Shoot();
    }

    //метод принимает направление и передаёт в скрипт 
    public void ChangeMovementDirection(Vector3 direction) 
    {
        PlayerMovementScript.ChangeMovementDirection(direction);
    }

    //метод принимает точку и передаёт в скрипт
    public void ChangeLookingPoint(Vector3 point) 
    {
        PlayerMovementScript.ChangeLookingPoint(point - transform.position);
    }

    //метод ускорение
    public void fastSpeedStart() //вызываем метод
    {
        PlayerMovementScript.fastSpeedStart();
    }

    public void fastSpeesEnd()
    {
        PlayerMovementScript.fastSpeesEnd();
    }
}
