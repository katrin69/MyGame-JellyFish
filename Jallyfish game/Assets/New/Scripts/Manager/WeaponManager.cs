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

    private void Awake()
    {
        WeaponBulletLight = GetComponent<WeaponBulletLight>();
        WeaponBulletJelly = GetComponent<WeaponBulletJelly>();
        WeaponJellyHoming = GetComponent<WeaponJellyHoming>();
        WeaponFart = GetComponent<WeaponFart>();
    }

}
