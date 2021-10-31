using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBulletLight : MonoBehaviour
{
    //Пуля молния с уроном 2
    private float damageEnemy = 2f; //урон
    private float bulletForce = 24f; //скорость
    private Rigidbody rb; //тело

    //таймер
    private float Timer;
    public float defaultTime = 8f;

    private LevelsSystem ShooterLevelSystem; //система лэвлов

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            this.ReturnToPool();
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
            EnemyHealth enemyHealthScript = other.transform.GetComponent<EnemyHealth>();
            //урон + опыт
            enemyHealthScript.DeductHealth(damageEnemy, ShooterLevelSystem);

            this.ReturnToPool();
        }
    }

    public void SetShooterLevelsSystem(LevelsSystem shooterLevelSystem) //передача лэвлов
    {
        ShooterLevelSystem = shooterLevelSystem;
    }
}
