using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBulletJelly : MonoBehaviour
{
    //Пуля медуза с уроном 1
    private float damageEnemy = 1f; //урон
    private float bulletForce = 24f; //скорость
    private Rigidbody rb; //тело

    //таймер
    private float Timer;
    public float defaultTime = 8f;

    private PlayerLevelSystem ShooterLevelSystem; //лэвлы

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
            //Скрипт здоровья акулы
            EnemyHealthScript enemyHealthScript = other.transform.GetComponent<EnemyHealthScript>();
            //урон + опыт
            enemyHealthScript.DeductHealth(damageEnemy * ShooterLevelSystem.CurrentLevel, ShooterLevelSystem);

            ResourceManager.ReturnToPool(gameObject);
        }
       
    }

    public void SetShooterLevelsSystem(PlayerLevelSystem shooterLevelSystem) //передаёт лэвлы
    {
        ShooterLevelSystem = shooterLevelSystem;
    }
}
