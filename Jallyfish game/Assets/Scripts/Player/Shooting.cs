using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; //молния
    public Transform firePoint; //место от куда стрелять
    public float bulletForce = 20f; //скорость движения молнии
    //Никита любит Лёшу

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) //если нажата кнопка то стрелять
        {
            Shoot();
        }

    }

    void Shoot() //метод стрельбы
    {
        GameObject bullet = ObjectPooler.instance.GetPooledObject(); //достаём пулю из пула

        if (bullet != null) //если пуля не пуста то 
        {
            LevelsSystem levelSystem = GetComponent<LevelsSystem>(); // достаём наши левлы ссылочный тип

            if (levelSystem != null) //если наш левл не пуст то 
            {
                BulletLight bulletLight = bullet.GetComponent<BulletLight>(); //достаём пулю
                bulletLight.SetShooterLevelsSystem(levelSystem);  //отдаёт систему левла на сохранение когда выпускает пулю
            }
            else
            {
                BulletJelly bulletLight = bullet.GetComponent<BulletJelly>(); //достаём пулю
                bulletLight.SetShooterLevelsSystem(levelSystem);  //отдаёт систему левла на сохранение когда выпускает пулю
            }

            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }
}
