using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletJellyHoming : MonoBehaviour
{
    [SerializeField] float damageEnemy = 3f; //Величина урона
    public float bulletSpeed = 14f;
    Transform target; //Наша цель 
    private Rigidbody rb;

    //таймер после которого пуля исчезает
    private float Timer; 
    public float defaultTime = 8f;

    //Движкник по кругу
    public float angle = 0; // угол 
    public float radius = 0.5f; // радиус
    public bool isCircle = false; // условие движения по кругу
    public float speed = 1f;
    public Vector3 cachedCenter;// запоминать свое нахождение и делать его центром окружности

    private LevelsSystem ShooterLevelSystem; //брём систему уровней

  
    private void Start()
    {
        //1
        target = GameObject.FindGameObjectWithTag("Shark").transform; //Цель равно обьекту с тэгом Акула 
        rb = GetComponent<Rigidbody>();

        //2
        //enemy = GameObject.FindGameObjectWithTag("Shark").transform;
        //target = new Vector3(enemy.position.x, enemy.position.y, enemy.position.z);

        //3
    }

    private void Update()
    {
        FoundEnemy();

        //2
        //transform.position = Vector3.MoveTowards(transform.position, enemy.position , bulletSpeed * Time.deltaTime); //перемещаемся к таргеты с опр скорость
        Timer -= Time.deltaTime; // Время после которого молния исчезает
        if (Timer < 0)
        {
            gameObject.SetActive(false);
        }

    }
    private void OnEnable()
    {
        Timer = defaultTime;
    }


    public void SetShooterLevelsSystem(LevelsSystem shooterLevelSystem) //система левлов на сохранение
    {
        ShooterLevelSystem = shooterLevelSystem;

    }

    public void FoundEnemy()
    {
        if (target.gameObject.CompareTag("Shark"))
        {
            Vector3 targetdirection = target.position - transform.position;
            transform.LookAt(target);
            rb.velocity = targetdirection.normalized * bulletSpeed;
        }
        else
        {
            var dx = Mathf.Cos(angle) * radius;
            var dz = Mathf.Sin(angle) * radius;

   //????        transform.position = cachedCenter + new Vector3(dx, 0, dz);

            angle += Time.deltaTime * speed;
            if (angle >= 360f)
                angle -= 360f;
        }
  
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
