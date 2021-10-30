using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //шарит за оружие
    private WeaponBulletLight WeaponBulletLight; //скрипты с оружием
    private WeaponBulletJelly WeaponBulletJelly;
    private WeaponJellyHoming WeaponJellyHoming;
    private WeaponFart WeaponFart;

    private IWeaponaBase activeWeapon;

    private void Awake()
    {
        WeaponBulletLight = GetComponent<WeaponBulletLight>();
        WeaponBulletJelly = GetComponent<WeaponBulletJelly>();
        WeaponJellyHoming = GetComponent<WeaponJellyHoming>();
        WeaponFart = GetComponent<WeaponFart>();
        activeWeapon.Shoot();
    }

    public void ChoosWeaponOne()
    {
        activeWeapon = WeaponBulletLight;
    }

    public void ChoosWeaponTwo()
    {
        activeWeapon = WeaponBulletJelly;

    }

    public void ChoosWeaponThree()
    {
        activeWeapon = WeaponJellyHoming;

    }

    public void ChoosWeaponFour()
    {
        activeWeapon = WeaponFart;

    }
}
