using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; //молния
    public Transform firePoint; //место от куда стрелять
    public float bulletForce = 20f; //скорость движения молнии
                                    //Никита любит Лёшу

    LevelsSystem levelSystem;

    //для большого пука
    [SerializeField] ParticleSystem _bulletFart;
    [SerializeField] float damageEnemyFart = 4f; //Величина урона


    private void Start()
    {
       levelSystem = GetComponent<LevelsSystem>(); // достаём наши левлы ссылочный тип

    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) //если нажата кнопка то стрелять
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShootFart();
        }


    }

    void Shoot() //метод стрельбы
    {
        GameObject bullet = ObjectPooler.instance.GetPooledObject(); //достаём пулю из пула

        if (bullet != null) //если пуля не пуста то 
        {

            if (levelSystem != null) //если наш левл не пуст то 
            {
                BulletLight bulletLight = bullet.GetComponent<BulletLight>(); //достаём пулю
                bulletLight.SetShooterLevelsSystem(levelSystem);  //отдаёт систему левла на сохранение когда выпускает пулю
            }
            //сделать другой метод
            //if (levelSystem != null)
            //{
            //    BulletJelly bulletJelly = bullet.GetComponent<BulletJelly>(); //достаём пулю
            //    bulletJelly.SetShooterLevelsSystem(levelSystem);  //отдаёт систему левла на сохранение когда выпускает пулю
            //}

            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }

    public void ShootFart()
    {
        _bulletFart.Play();

        Collider[] colliders = Physics.OverlapSphere(transform.position, 6f);
        foreach (Collider c in colliders)
        {
            if (c.gameObject.CompareTag("Shark"))
            {
                if (levelSystem != null)
                {

                    ////Получаем скрипт здоровья акулы
                    EnemyHealth enemyHealthScript = c.transform.GetComponent<EnemyHealth>();
                    //передаём урон
                    enemyHealthScript.DeductHealth(damageEnemyFart, levelSystem);

                }
            }
        }
    }
}
