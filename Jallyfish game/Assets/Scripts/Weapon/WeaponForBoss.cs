using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponForBoss : MonoBehaviour
{
    //Пуля для босса
   // private float damageEnemy = 4f; //урон
    private float bulletForce = 30f; //скорость
    private Rigidbody rb; //тело

    //таймер
    private float Timer;
    public float defaultTime = 8f;

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

    //если сталкиваемся с медузой
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //отнимаем жизнь у медузы
            collision.gameObject.GetComponent<PlayerHealthScript>().RecountArmorp(-2);
        }
    }
}
