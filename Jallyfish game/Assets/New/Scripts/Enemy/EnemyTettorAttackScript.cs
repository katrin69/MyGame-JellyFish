using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTettorAttackScript : MonoBehaviour
{
    // public GameObject shark, explosion;
    [SerializeField] ParticleSystem _explosion;
    public float moveSpeed = 6f; // скорость врага
    private Rigidbody rb;

    private float dis;
    public float howClose;
    private Transform player;


    //private void Awake()
    //{
    //    shark.SetActive(true);
    //    explosion.SetActive(false);
    //}
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
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


    //если сталкиваемся с медузой
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //shark.SetActive(false);
            //explosion.SetActive(true);
            _explosion.Play();
            //отнимаем жизнь у медузы
            collision.gameObject.GetComponent<PlayerHealthScript>().RecountArmorp(-4);

          
          //  ResourceManager.ReturnToPool(gameObject);
            

        }
    }
}
