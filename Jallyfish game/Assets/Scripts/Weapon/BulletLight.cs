using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLight : MonoBehaviour
{
    private float bulletForce = 24f; //сила пули
    [SerializeField] private Rigidbody rb; //тело пули
    public int damage; //Величина урона
    public Vector3 dir;

    private float Timer; //таймер после которого пуля исчезает
    public float defaultTime = 8f;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            return; //ничего не делает. Выходит из метода
        }


        gameObject.SetActive(false); //удаляем молнию

        //Отправляет сообщение в лог с тегом врага
        Debug.Log(other.transform.tag);


        if (other.gameObject.CompareTag("Shark"))
        {
            ////Получаем скрипт EnemyHealth с объекта коллизии
            //EnemyHealth healthScript = other.transform.GetComponent<EnemyHealth>();

            //if (healthScript)
            //{
            //    healthScript.health -= damage;  //Делаем урон врагу

            //    if (healthScript.health < 0) //Если хп стало меньше нуля, то ставим 0
            //    {
            //        healthScript.health = 0;
            //    }

            //}
            //else
            //{

            //    Debug.Log("No scripts");
            //}

            Destroy(other.gameObject,5f);//Удаляем объект
        }
    }

}
