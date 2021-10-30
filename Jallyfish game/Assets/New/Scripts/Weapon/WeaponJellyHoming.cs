using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponJellyHoming : MonoBehaviour, IWeaponaBase
{
    //Самонаводящееся медуза c уроном 3
    private float damageEnemy = 3f; //урон
    private float bulletForce = 24f; //скорость
    Transform target; //цель
    public float radius = 3.5f; //радиус
    private Rigidbody rb;

    //таймер
    private float Timer;
    public float defaultTime = 10f;

    //поиск врага
    private float SearchTimer = 0;
    private float SearchStep = 1;

    private Transform MotherJellyfish; //наша мама (игрок)

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Initialize(Transform jellyfishParent)
    {
        MotherJellyfish = jellyfishParent;
    }

    private void OnEnable()
    {
        Timer = defaultTime;
    }

    private void Update()
    {
        if (target != null) //если цель не пуста то идём на цель
        {
            Vector3 targetdirection = target.position - transform.position;
            transform.LookAt(target);
            rb.velocity = targetdirection.normalized * bulletForce;
        }
        else //иначе исчезаем через какое то вемя И нарезаем круг вокруг нашей медузы
        {
            SearchTimer += Time.deltaTime;

            if (SearchTimer >= SearchStep) //при этом ищем нашего врага каждую секуду
            {
                SearchTimer = 0; 
                FoundEnemy(); //нашёл врага
            }

            Vector3 targetdirection = MotherJellyfish.position - transform.position; //указывает направление к игроку. К кому от кого

            if (targetdirection.magnitude <= radius) //если длина вектора меньше или равно радиусу то создаём вектор kasatka
            {
                Vector3 kasatka = Vector3.Cross(Vector3.up, targetdirection); //произведение двух векторов
                rb.velocity = kasatka.normalized * bulletForce; //
            }
            else
            {
                transform.LookAt(MotherJellyfish); //иначе смотрит на игрока 
                rb.velocity = targetdirection.normalized * bulletForce;
            }
        }

        DeathCountdown();//пуля исчезает
    }

    private void DeathCountdown()
    {
        Timer -= Time.deltaTime;

        if (Timer < 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void FoundEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15f); //массив колайдеров вокруг

        Collider nearest = null; //рядом пока пусто с самого начала

        foreach (Collider collider in colliders) 
        {
            if (collider.gameObject.CompareTag("Shark")) //если в этом массиве есть Акула
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

    private void OnDisable()
    {
        target = null;
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

    public void Shoot()
    {
        throw new System.NotImplementedException();
    }
}
