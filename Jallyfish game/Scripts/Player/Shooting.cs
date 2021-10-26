using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; //молния
    public Transform firePoint; //место от куда стрелять
    public float bulletForce = 20f; //скорость движения молнии
                                    

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

            ShootBulletHoming();
            // ShootBulletLight();
            // ShootBulletJelly();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShootFart();
        }


    }

    void ShootBulletLight() //стреляю молниями
    {
        GameObject bullet = ObjectPooler.instance.GetPooledObject(); //достаём пулю из пула

        if (bullet != null) //если пуля не пуста то 
        {

            if (levelSystem != null) //если наш левл не пуст то 
            {
                BulletLight bulletLight = bullet.GetComponent<BulletLight>(); //достаём пулю
                bulletLight.SetShooterLevelsSystem(levelSystem);  //отдаёт систему левла на сохранение когда выпускает пулю
            }

            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }


    void ShootBulletJelly() //стреляю медузами
    {
        GameObject bullet = ObjectPooler.instance.GetPooledObject(); //достаём пулю из пула

        if (levelSystem != null)
        {
            BulletJelly bulletJelly = bullet.GetComponent<BulletJelly>(); //достаём пулю
            bulletJelly.SetShooterLevelsSystem(levelSystem);  //отдаёт систему левла на сохранение когда выпускает пулю
        }
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetActive(true);
    }


    void ShootBulletHoming() //самонаводящиеся
    {
        GameObject bullet = ObjectPooler.instance.GetPooledObject(); //достаём пулю из пула

        if (bullet != null) //если пуля не пуста то 
        {

            if (levelSystem != null) //если наш левл не пуст то 
            {
                BulletJellyHoming bulletLight = bullet.GetComponent<BulletJellyHoming>(); //достаём пулю
                bulletLight.Initialize(transform, levelSystem);  //отдаёт систему левла на сохранение когда выпускает пулю
            }

            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }


    public void ShootFart() //Большой Пук
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
