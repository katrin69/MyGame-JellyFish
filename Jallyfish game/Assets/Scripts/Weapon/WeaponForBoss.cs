using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponForBoss : MonoBehaviour
{
    public EnemyHealthScript EnemyHealthScript;

    //Самонаводящееся акула c уроном 3
    public float damageEnemy = 3f; //урон
    public float bulletSpeed = 13f; //скорость
    public Transform target; //цель
    private Rigidbody rb;

    //таймер
    private float Timer;
    public float defaultTime = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        EnemyHealthScript.HealthPercentageChanged += EnemyHealthScript_HealthPercentageChanged; ;
    }

    private void EnemyHealthScript_HealthPercentageChanged(EnemyHealthScript arg1, float arg2)
    {
        if (arg2 <= 0)
        {
            ResourceManager.ReturnToPool(gameObject);
        }
    }

    private void OnEnable()
    {
        Timer = defaultTime;
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 targetdirection = target.position - transform.position;
            transform.LookAt(target);
            rb.velocity = targetdirection.normalized * bulletSpeed;
        }

        DeathCountdown();//пуля исчезает
    }

    private void DeathCountdown()
    {
        Timer -= Time.deltaTime;

        if (Timer < 0)
        {
            ResourceManager.ReturnToPool(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //отнимаем жизнь у медузы
            collision.gameObject.GetComponent<PlayerHealthScript>().RecountArmorp(-damageEnemy);
            ResourceManager.ReturnToPool(gameObject);
        }
    }
}
