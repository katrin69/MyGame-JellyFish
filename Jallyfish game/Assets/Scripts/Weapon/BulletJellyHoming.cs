using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletJellyHoming : MonoBehaviour
{
    //[SerializeField] float damageEnemy = 3f; //Величина урона
    public float bulletSpeed = 10f;
    public GameObject bulletHoming;
    public GameObject target;
    //public List<GameObject> spawn;


    //// Transform target; //Наша цель 
    //private Rigidbody rb;
    //private Vector3 target;
    //Transform enemy;

    private LevelsSystem ShooterLevelSystem; //брём систему уровней


    private void Start()
    {
        //1
        //target = GameObject.FindGameObjectWithTag("Shark").transform; //Цель равно обьекту с тэгом Акула 
        //rb = GetComponent<Rigidbody>();

        //2
        //enemy = GameObject.FindGameObjectWithTag("Shark").transform;
        //target = new Vector3(enemy.position.x, enemy.position.y, enemy.position.z);

        //3
    }

    private void Update()
    {
        //2
        //transform.position = Vector3.MoveTowards(transform.position, enemy.position , bulletSpeed * Time.deltaTime); //перемещаемся к таргеты с опр скорость

        //3
        bulletHoming.transform.LookAt(target.transform);
        StartCoroutine(SendHoming(bulletHoming));
    }


    //1
    //private void FixedUpdate()
    // {
    //Vector3 targetdirection = target.position - transform.position;
    //transform.LookAt(target);
    //rb.velocity = targetdirection.normalized * bulletSpeed;
   // }

    public IEnumerator SendHoming(GameObject bulletHoming)
    {
        while (Vector3.Distance(target.transform.position, bulletHoming.transform.position) > 0.3f)
        {
            bulletHoming.transform.position += (target.transform.position - bulletHoming.transform.position).normalized * bulletSpeed * Time.deltaTime;
            bulletHoming.transform.LookAt(target.transform);
            yield return null;
        }
        Destroy(bulletHoming);
    }

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
