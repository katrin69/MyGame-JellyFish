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
    private float Timer; 
    public float defaultTime = 8f;

    //�������� �� �����
    public float angle = 0; // ���� 
    public float radius = 0.5f; // ������
    public bool isCircle = false; // ������� �������� �� �����
    public float speed = 1f;
    public Vector3 cachedCenter;// ���������� ���� ���������� � ������ ��� ������� ����������

    private LevelsSystem ShooterLevelSystem; //��� ������� �������

  
    private void Start()
    {
        //1
        target = GameObject.FindGameObjectWithTag("Shark").transform; //���� ����� ������� � ����� ����� 
        rb = GetComponent<Rigidbody>();

        //2
        //enemy = GameObject.FindGameObjectWithTag("Shark").transform;
        //target = new Vector3(enemy.position.x, enemy.position.y, enemy.position.z);

        //3
    }

    private void Update()
    {
        FoundEnemy();

        //2
        //transform.position = Vector3.MoveTowards(transform.position, enemy.position , bulletSpeed * Time.deltaTime); //������������ � ������� � ��� ��������
        Timer -= Time.deltaTime; // ����� ����� �������� ������ ��������
        if (Timer < 0)
        {
            gameObject.SetActive(false);
        }

    }
    private void OnEnable()
    {
        Timer = defaultTime;
    }


    public void SetShooterLevelsSystem(LevelsSystem shooterLevelSystem) //������� ������ �� ����������
    {
        ShooterLevelSystem = shooterLevelSystem;

    }

    public void FoundEnemy()
    {
        if (target.gameObject.CompareTag("Shark"))
        {
            Vector3 targetdirection = target.position - transform.position;
            transform.LookAt(target);
            rb.velocity = targetdirection.normalized * bulletSpeed;
        }
        else
        {
            var dx = Mathf.Cos(angle) * radius;
            var dz = Mathf.Sin(angle) * radius;

   //????        transform.position = cachedCenter + new Vector3(dx, 0, dz);

            angle += Time.deltaTime * speed;
            if (angle >= 360f)
                angle -= 360f;
        }
  
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            return; //������ �� ������. ������� �� ������
        }
        gameObject.SetActive(false); //������� ������

        if (other.gameObject.CompareTag("Shark")) //���� ���� ����������� � ������
        {
            ////�������� ������ �������� �����
            EnemyHealth enemyHealthScript = other.transform.GetComponent<EnemyHealth>();
            //������� ����
            enemyHealthScript.DeductHealth(damageEnemy, ShooterLevelSystem);
        }
    }
}
