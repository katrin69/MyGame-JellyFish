using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletJelly : MonoBehaviour
{
    private float bulletForce = 24f; //сила пули
    [SerializeField] private Rigidbody rb; //тело пули
    [SerializeField] float damageEnemy = 1f; //Величина урона

    public Vector3 dir;



    private float Timer; //таймер после которого пуля исчезает
    public float defaultTime = 8f;

    private LevelsSystem ShooterLevelSystem; //брём систему уровней

    private void Update() // Время после которого молния исчезает
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            gameObject.SetActive(false);
        }
    }
    //Никита любит Лёшу

    private void OnEnable()
    {
        Timer = defaultTime;
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * bulletForce; //движение молнии
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

        //Отправляет сообщение в лог с тегом врага
        Debug.Log(other.transform.tag);


        if (other.gameObject.CompareTag("Shark")) //если пуля столкнулась с Акулой
        {
            ////Получаем скрипт здоровья акулы
            EnemyHealth enemyHealthScript = other.transform.GetComponent<EnemyHealth>();
            //передаём урон
            enemyHealthScript.DeductHealth(damageEnemy, ShooterLevelSystem);
        }
    }

}
