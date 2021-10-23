using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletJellyHoming : MonoBehaviour
{
    [SerializeField] float damageEnemy = 3f; //Величина урона
    public float bulletSpeed = 10f;

    // Transform target; //Наша цель 
    private Rigidbody rb;
    private Vector3 target;
    Transform enemy;

    private LevelsSystem ShooterLevelSystem; //брём систему уровней


    private void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Shark").transform; //Цель равно обьекту с тэгом Акула 
        //rb = GetComponent<Rigidbody>();

        enemy = GameObject.FindGameObjectWithTag("Shark").transform;
        target = new Vector3(enemy.position.x, enemy.position.y, enemy.position.z);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, enemy.position , bulletSpeed * Time.deltaTime); //перемещаемся к таргеты с опр скорость
    }

    //private void FixedUpdate()
    // {
    //Vector3 targetdirection = target.position - transform.position;
    //transform.LookAt(target);
    //rb.velocity = targetdirection.normalized * bulletSpeed;


    // }

    public void SetShooterLevelsSystem(LevelsSystem shooterLevelSystem) //система левлов на сохранение
    {
        ShooterLevelSystem = shooterLevelSystem;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            return; //ничего не делает. Выходит из метода
        }
        gameObject.SetActive(false); //удаляем молнию

        if (other.gameObject.CompareTag("Shark")) //если пуля столкнулась с Акулой
        {
            ////Получаем скрипт здоровья акулы
            EnemyHealth enemyHealthScript = other.transform.GetComponent<EnemyHealth>();
            //передаём урон
            enemyHealthScript.DeductHealth(damageEnemy, ShooterLevelSystem);
        }
    }
}
