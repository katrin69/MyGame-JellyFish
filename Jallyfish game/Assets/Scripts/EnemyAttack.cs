using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
   // public Transform player; //Наша медуза
    public float moveSpeed = 10f; // Скорость движения
    private Rigidbody rb;



    private float dis; //расстояние между Акулой и Медузой
    public float howClose; //насколько Акула близка к Медузе
    private Transform player;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //dis = Vector3.Distance(player.position, transform.position);
        Vector3 targetdirection = player.position - transform.position;
        if (targetdirection.magnitude > 0.5f) // если расстояние больше 0.5
        {
            transform.LookAt(player);
            rb.velocity = targetdirection.normalized * moveSpeed; // то задаем объекту скорость по направлению к конечной точке
        }


        if (dis <= howClose)
        {
            transform.LookAt(player);
            GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed);
        }

    }

    //Если Акула сталкивается с Медузой
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Обращаемся к Медузе
            collision.gameObject.GetComponent<PlayerHealth>().RecountHp(-1);
            print("Никита любит Лёшу");
        }
    }



}
