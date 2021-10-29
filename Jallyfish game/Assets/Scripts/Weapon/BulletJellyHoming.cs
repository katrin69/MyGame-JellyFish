using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletJellyHoming : MonoBehaviour
{
    [SerializeField] float damageEnemy = 3f; //урон
    public float bulletSpeed = 14f;
    Transform target; //цель
    private Rigidbody rb;

    //таймер исчезновения
    private float DeathTimer;
    public float defaultTime = 15f;

    
    public float radius = 3.5f; //радиус

    private LevelsSystem ShooterLevelSystem; //система лэвлов

    private float SearchTimer = 0;
    private float SearchStep = 1;

    private Transform MotherJellyfish;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Initialize(Transform jellyfishParent, LevelsSystem shooterLevelSystem) 
    {
        MotherJellyfish = jellyfishParent;
        ShooterLevelSystem = shooterLevelSystem;
    }

    private void OnEnable()
    {
        DeathTimer = defaultTime;
    }

    private void Update()
    {
        if (target != null)
        {
             Vector3 targetdirection = target.position - transform.position;
             transform.LookAt(target);
             rb.velocity = targetdirection.normalized * bulletSpeed;
        }
        else
        {
            SearchTimer += Time.deltaTime;

            if (SearchTimer >= SearchStep)
            {
                SearchTimer = 0;
                FoundEnemy();
            }

            Vector3 targetdirection = MotherJellyfish.position - transform.position;

            if (targetdirection.magnitude <= radius)
            {
                Vector3 kasatka = Vector3.Cross(Vector3.up, targetdirection);
                rb.velocity = kasatka.normalized * bulletSpeed;
            }
            else
            {
                transform.LookAt(MotherJellyfish);
                rb.velocity = targetdirection.normalized * bulletSpeed;
            }
        }

        DeathCountdown();
    }

    private void DeathCountdown()
    {
        DeathTimer -= Time.deltaTime; 

        if (DeathTimer < 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void FoundEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15f); //массив колайдеров вокруг

        Collider nearest = null;

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
        if (other.gameObject.CompareTag("Shark")) 
        {
            gameObject.SetActive(false); 

            //скрипт акулы
            EnemyHealth enemyHealthScript = other.transform.GetComponent<EnemyHealth>();
            //урон + опыт
            enemyHealthScript.DeductHealth(damageEnemy, ShooterLevelSystem);
        }
    }
}
