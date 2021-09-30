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
       rb.velocity = Vector3.forward * bulletForce; //движение молнии
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false); //удаляем молнию

        //Отправляет сообщение в лог с тегом врага
        Debug.Log(collision.transform.tag);

        if (collision.gameObject.CompareTag("Shark"))
        {
            //Получаем скрипт EnemyHealth с объекта коллизии
            EnemyHealth healthScript = collision.transform.GetComponent<EnemyHealth>();

            if (healthScript)
            {
                healthScript.health -= damage;  //Делаем урон врагу

                if (healthScript.health < 0) //Если хп стало меньше нуля, то ставим 0
                {
                    healthScript.health = 0;
                }
               
            }
            else
            {

                Debug.Log("No scripts");
            }
            Destroy(collision.gameObject);//Удаляем объект
        }
        
    }
}
