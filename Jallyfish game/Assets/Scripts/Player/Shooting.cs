using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; //берём пулю
    public Transform firePoint; //место от куда стрелять
    public float bulletForce = 20f; //скорость пули
                                    

    LevelsSystem levelSystem; //система лэвлов

    //для большого пука
    [SerializeField] ParticleSystem _bulletFart;
    [SerializeField] float damageEnemyFart = 4f; //урон


    private void Start()
    {
       levelSystem = GetComponent<LevelsSystem>(); // берём систему лэвлов

    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) //
        {

            //ShootBulletHoming();
            // ShootBulletLight();
             ShootBulletJelly();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShootFart();
        }


    }

    void ShootBulletLight() //удар молнии
    {
        GameObject bullet = ObjectPooler.instance.GetPooledObject(); //берём пулю из пула

        if (bullet != null) //если пуля не пуста
        {

            if (levelSystem != null) //и система лэвлов не пуста
            {
                BulletLight bulletLight = bullet.GetComponent<BulletLight>(); //то берём скрипт пули
                bulletLight.SetShooterLevelsSystem(levelSystem);  //и метод из лэвлов
            }

            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }


    void ShootBulletJelly() //метод пули медуза
    {
        GameObject bullet = ObjectPooler.instance.GetPooledObject(); //берём пулю из пула

        if (bullet != null) //если пуля не пуста
        {
            if (levelSystem != null)//и система лэвлов не пуста
            {
                BulletJelly bulletJelly = bullet.GetComponent<BulletJelly>(); //то берём скрипт пули
                bulletJelly.SetShooterLevelsSystem(levelSystem);  //и метод из лэвлов
            }

        }
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetActive(true);
    }


    void ShootBulletHoming() //Самонаводящееся пуля
    {
        GameObject bullet = ObjectPooler.instance.GetPooledObject(); ///берём пулю из пула

        if (bullet != null) // если пуля не пуста
        {

            if (levelSystem != null) //и система лэвлов не пуста
            {
                BulletJellyHoming bulletLight = bullet.GetComponent<BulletJellyHoming>(); //то берём скрипт пули
                bulletLight.Initialize(transform, levelSystem);  //и метод из лэвлов
            }

            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }


    public void ShootFart() //Большой пук
    {
        _bulletFart.Play();

        Collider[] colliders = Physics.OverlapSphere(transform.position, 6f);
        foreach (Collider c in colliders)
        {
            if (c.gameObject.CompareTag("Shark"))
            {
                if (levelSystem != null)
                {
                    //скрипт врагов
                    EnemyHealth enemyHealthScript = c.transform.GetComponent<EnemyHealth>();
                    //урон + опыт
                    enemyHealthScript.DeductHealth(damageEnemyFart, levelSystem);

                }
            }
        }
    }
}
