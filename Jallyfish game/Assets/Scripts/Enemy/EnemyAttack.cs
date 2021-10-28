using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
   // public Transform player;
    public float moveSpeed = 10f; // скорость врага
    private Rigidbody rb;



    private float dis; 
    public float howClose; 
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
        if (targetdirection.magnitude > 0.5f) // если расстояние меньше то 
        {
            transform.LookAt(player);
            rb.velocity = targetdirection.normalized * moveSpeed; // поворачиваемся и движемся к медузе
        }


        if (dis <= howClose)
        {
            transform.LookAt(player);
            GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed);
        }

    }

    //Åñëè Àêóëà ñòàëêèâàåòñÿ ñ Ìåäóçîé
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Îáðàùàåìñÿ ê Ìåäóçå ê áðîíå
            collision.gameObject.GetComponent<PlayerHealth>().RecountArmorp(-1);


        }
    }



}
