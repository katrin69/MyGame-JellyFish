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

            ShootBulletHoming();
            // ShootBulletLight();
            // ShootBulletJelly();
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


    void ShootBulletHoming() //Большой Пук
    {
        GameObject bullet = ObjectPooler.instance.GetPooledObject(); //äîñòà¸ì ïóëþ èç ïóëà

        if (bullet != null) //åñëè ïóëÿ íå ïóñòà òî 
        {

            if (levelSystem != null) //åñëè íàø ëåâë íå ïóñò òî 
            {
                BulletJellyHoming bulletLight = bullet.GetComponent<BulletJellyHoming>(); //äîñòà¸ì ïóëþ
                bulletLight.Initialize(transform, levelSystem);  //îòäà¸ò ñèñòåìó ëåâëà íà ñîõðàíåíèå êîãäà âûïóñêàåò ïóëþ
            }

            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }


    public void ShootFart() //Áîëüøîé Ïóê
    {
        _bulletFart.Play();

        Collider[] colliders = Physics.OverlapSphere(transform.position, 6f);
        foreach (Collider c in colliders)
        {
            if (c.gameObject.CompareTag("Shark"))
            {
                if (levelSystem != null)
                {
                    ////Ïîëó÷àåì ñêðèïò çäîðîâüÿ àêóëû
                    EnemyHealth enemyHealthScript = c.transform.GetComponent<EnemyHealth>();
                    //ïåðåäà¸ì óðîí
                    enemyHealthScript.DeductHealth(damageEnemyFart, levelSystem);

                }
            }
        }
    }
}
