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

    private void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            gameObject.SetActive(false);
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
        if (other.gameObject.CompareTag("Player"))
        {
            return;
        }

        gameObject.SetActive(false);

        if (other.gameObject.CompareTag("Shark")) //если столкнулась с акулой
        {
            //урон акуле
        }
    }
}
