using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackScript : MonoBehaviour
{
    // public Transform player;
    public float moveSpeed = 10f; // скорость врага
    private Rigidbody rb;

    private float dis;
    public float howClose;
    private Transform player;

    Transform target; //цель Медуза
    //поиск Медузы
    private float SearchTimer = 0;
    private float SearchStep = 1;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (target != null) //если цель не пуста то идём на цель
        {
            Vector3 targetdirection = target.position - transform.position; //расстояние до цели
            transform.LookAt(target);
            rb.velocity = targetdirection.normalized * moveSpeed;
        }
        else
        {
            SearchTimer += Time.deltaTime;

            if (SearchTimer >= SearchStep) //при этом ищем медузу каждую секунду
            {
                SearchTimer = 0;
                FoundJellyfish(); //нашёл медузу
            }
        }

    }

    public void FoundJellyfish()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 30f); //массив колайдеров вокруг

        Collider nearest = null; //рядом пока пусто с самого начала

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player")) //если в этом массиве есть Акула
            {
                if (nearest == null) //если не очень близко то пофиг
                {
                    nearest = collider;
                }
                else
                {
                    if ((collider.transform.position - transform.position).magnitude < (nearest.transform.position - transform.position).magnitude) //если близко то ударить
                    {
                        nearest = collider;
                    }
                }
            }
        }

        if (nearest != null) //цель станогвится ближе
        {
            target = nearest.transform;
        }
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
