using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //шарит за оружие
    public Transform firePoint;

    public EWeapon currentWeapon;

    private ResourceManager ResourceManager;
    private LevelsSystem LevelsSystem;

    private Dictionary<EWeapon, float> WeaponsCurrentCooldown = new Dictionary<EWeapon, float>();

    private Dictionary<EWeapon, float> WeaponsDefaultCooldown = new Dictionary<EWeapon, float>()
    {
        { EWeapon.BulletLight, 0.5f },
        { EWeapon.BulletJelly, 0.8f },
        { EWeapon.JellyHoming, 1.5f },
        { EWeapon.Fart, 5f }
    };

    private void Awake()
    {
        currentWeapon = EWeapon.BulletLight;
    }

    private void Update()
    {
        foreach (EWeapon eWeapon in WeaponsDefaultCooldown.Keys)
        {
            if (WeaponsCurrentCooldown.ContainsKey(eWeapon))
            {
                if (WeaponsCurrentCooldown[eWeapon] < 0)
                {
                    WeaponsCurrentCooldown[eWeapon] = 0;
                }
                else if (WeaponsCurrentCooldown[eWeapon] > 0)
                {
                    WeaponsCurrentCooldown[eWeapon] -= Time.deltaTime;
                }
            }
        }
    }

    public void Init(ResourceManager resourceManager, LevelsSystem levelsSystem)
    {
        ResourceManager = resourceManager;
        LevelsSystem = levelsSystem;
    }

    public void ChoosWeaponOne()
    {
        currentWeapon = EWeapon.BulletLight;
    }

    public void ChoosWeaponTwo()
    {
        currentWeapon = EWeapon.BulletJelly;
    }

    public void ChoosWeaponThree()
    {
        currentWeapon = EWeapon.JellyHoming;
    }

    public void ChoosWeaponFour()
    {
        currentWeapon = EWeapon.Fart;
    }

    public void Shoot()
    {
        if (WeaponsCurrentCooldown.ContainsKey(currentWeapon) == false)
        {
            WeaponsCurrentCooldown.Add(currentWeapon, 0);
        }

        if (WeaponsCurrentCooldown[currentWeapon] <= 0)
        {
            switch (currentWeapon)
            {
                case EWeapon.BulletLight:
                    ShootBulletLight();
                    break;
                case EWeapon.BulletJelly:
                    ShootBulletJelly();
                    break;
                case EWeapon.JellyHoming:
                    ShootBulletHoming();
                    break;
                case EWeapon.Fart:
                    ShootFart();
                    break;
            }

            WeaponsCurrentCooldown[currentWeapon] = WeaponsDefaultCooldown[currentWeapon];
        }
    }

    private void ShootBulletLight() //удар молнии
    {
        GameObject bullet = ResourceManager.GetObjectInstance(EObjectType.BulletLight); //берём пулю из пула

        if (bullet != null) //если пуля не пуста
        {
            if (LevelsSystem != null) //и система лэвлов не пуста
            {
                WeaponBulletLight bulletLight = bullet.GetComponent<WeaponBulletLight>(); //то берём скрипт пули
                bulletLight.SetShooterLevelsSystem(LevelsSystem);  //и метод из лэвлов
            }

            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }

    private void ShootBulletJelly() //метод пули медуза
    {
        GameObject bullet = ResourceManager.GetObjectInstance(EObjectType.BulletJelly); //берём пулю из пула

        if (bullet != null) //если пуля не пуста
        {
            if (LevelsSystem != null)//и система лэвлов не пуста
            {
                WeaponBulletJelly bulletJelly = bullet.GetComponent<WeaponBulletJelly>(); //то берём скрипт пули
                bulletJelly.SetShooterLevelsSystem(LevelsSystem);  //и метод из лэвлов
            }
        }

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetActive(true);
    }

    private void ShootBulletHoming() //Самонаводящееся пуля
    {
        GameObject bullet = ResourceManager.GetObjectInstance(EObjectType.JellyHoming); //берём пулю из пула

        if (bullet != null) // если пуля не пуста
        {
            if (LevelsSystem != null) //и система лэвлов не пуста
            {
                WeaponJellyHoming bulletHoming = bullet.GetComponent<WeaponJellyHoming>(); //то берём скрипт пули
                bulletHoming.Initialize(transform, LevelsSystem);  //и метод из лэвлов
            }

            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }

    private void ShootFart() //Большой пук
    {
        GameObject bullet = ResourceManager.GetObjectInstance(EObjectType.BulletFart); //берём пулю из пула

        if (bullet != null) // если пуля не пуста
        {
            if (LevelsSystem != null) //и система лэвлов не пуста
            {
                WeaponFart bulletHoming = bullet.GetComponent<WeaponFart>(); //то берём скрипт пули
                bulletHoming.SetShooterLevelsSystem(LevelsSystem);  //и метод из лэвлов
            }

            bullet.transform.position = transform.position;
            bullet.SetActive(true);
        }
    }
}
