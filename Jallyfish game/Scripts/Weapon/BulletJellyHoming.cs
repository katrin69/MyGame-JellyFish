using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletJellyHoming : MonoBehaviour
{
    [SerializeField] float damageEnemy = 3f; //�������� �����
    public float bulletSpeed = 14f;
    Transform target; //���� ���� 
    private Rigidbody rb;

    //������ ����� �������� ���� ��������
    private float DeathTimer;
    public float defaultTime = 15f;

    //�������� �� �����
    //public float angle = 0; // ���� 
    public float radius = 3.5f; // ������
    //public bool isCircle = false; // ������� �������� �� �����
    //public float speed = 1f;
    //public Vector3 cachedCenter;// ���������� ���� ���������� � ������ ��� ������� ����������

    private LevelsSystem ShooterLevelSystem; //��� ������� �������

    private float SearchTimer = 0;
    private float SearchStep = 1;

    private Transform MotherJellyfish;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Initialize(Transform jellyfishParent, LevelsSystem shooterLevelSystem) //������� ������ �� ����������
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
        DeathTimer -= Time.deltaTime; // ����� ����� �������� ������ ��������

        if (DeathTimer < 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void FoundEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15f);

        Collider nearest = null;

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Shark"))
            {
                if (nearest == null)
                {
                    nearest = collider;
                }
                else
                {
                    if ((collider.transform.position - transform.position).magnitude < (nearest.transform.position - transform.position).magnitude)
                    {
                        nearest = collider;
                    }
                }
            }
        }

        if (nearest != null)
        {
            target = nearest.transform;
        }
    }

    private void OnDisable()
    {
         target = null;
    }

    // private void Start()
    // {
    //     //1
    //     target = GameObject.FindGameObjectWithTag("Shark").transform; //���� ����� ������� � ����� ����� 
    //     rb = GetComponent<Rigidbody>();

    //     //2
    //     //enemy = GameObject.FindGameObjectWithTag("Shark").transform;
    //     //target = new Vector3(enemy.position.x, enemy.position.y, enemy.position.z);

    //     //3
    // }

    // private void Update()
    // {
    //     FoundEnemy();

    //     //2
    //     //transform.position = Vector3.MoveTowards(transform.position, enemy.position , bulletSpeed * Time.deltaTime); //������������ � ������� � ��� ��������
    //     Timer -= Time.deltaTime; // ����� ����� �������� ������ ��������
    //     if (Timer < 0)
    //     {
    //         gameObject.SetActive(false);
    //     }

    // }





    // public void FoundEnemy()
    // {
    //     if (target)
    //     {
    //         Vector3 targetdirection = target.position - transform.position;
    //         transform.LookAt(target);
    //         rb.velocity = targetdirection.normalized * bulletSpeed;
    //     }
    //     else
    //     {
    //         var dx = Mathf.Cos(angle) * radius;
    //         var dz = Mathf.Sin(angle) * radius;

    ////????        transform.position = cachedCenter + new Vector3(dx, 0, dz);

    //         angle += Time.deltaTime * speed;
    //         if (angle >= 360f)
    //             angle -= 360f;
    //     }

    // }


    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Player"))
        //{
        //    return; //������ �� ������. ������� �� ������
        //}

        if (other.gameObject.CompareTag("Shark")) //���� ���� ����������� � ������
        {
            gameObject.SetActive(false); //������� ������

            ////�������� ������ �������� �����
            EnemyHealth enemyHealthScript = other.transform.GetComponent<EnemyHealth>();
            //������� ����
            enemyHealthScript.DeductHealth(damageEnemy, ShooterLevelSystem);
        }
    }
}
