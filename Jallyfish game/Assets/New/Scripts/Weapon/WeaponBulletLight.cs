using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBulletLight : MonoBehaviour
{
    //Пуля молния с уроном 2
    private float damageEnemy = 2f; //урон
    private float bulletForce = 30f; //скорость
    private Rigidbody rb; //тело

    //таймер
    private float Timer;
    public float defaultTime = 8f;

    private PlayerLevelSystem ShooterLevelSystem; //система лэвлов

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            ResourceManager.ReturnToPool(gameObject);
        }
    }
 
    private void OnEnable()
    {
        Timer = defaultTime;
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * bulletForce; //направление и скорость
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shark")) //если столкнулась с акулой
        {
            //урон акуле

            //Скрипт акулы
            EnemyHealthScript enemyHealthScript = other.transform.GetComponent<EnemyHealthScript>();
            //урон + опыт
            enemyHealthScript.DeductHealth(damageEnemy, ShooterLevelSystem);

            ResourceManager.ReturnToPool(gameObject);
        }
    }

    public void SetShooterLevelsSystem(PlayerLevelSystem shooterLevelSystem) //передача лэвлов
    {
        ShooterLevelSystem = shooterLevelSystem;
    }
}
