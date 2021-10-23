using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletJellyHoming : MonoBehaviour
{
    [SerializeField] float damageEnemy = 3f; //�������� �����
    public float bulletSpeed = 10f;

    // Transform target; //���� ���� 
    private Rigidbody rb;
    private Vector3 target;
    Transform enemy;

    private LevelsSystem ShooterLevelSystem; //��� ������� �������


    private void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Shark").transform; //���� ����� ������� � ����� ����� 
        //rb = GetComponent<Rigidbody>();

        enemy = GameObject.FindGameObjectWithTag("Shark").transform;
        target = new Vector3(enemy.position.x, enemy.position.y, enemy.position.z);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, enemy.position , bulletSpeed * Time.deltaTime); //������������ � ������� � ��� ��������
    }

    //private void FixedUpdate()
    // {
    //Vector3 targetdirection = target.position - transform.position;
    //transform.LookAt(target);
    //rb.velocity = targetdirection.normalized * bulletSpeed;


    // }

    public void SetShooterLevelsSystem(LevelsSystem shooterLevelSystem) //������� ������ �� ����������
    {
        ShooterLevelSystem = shooterLevelSystem;

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
