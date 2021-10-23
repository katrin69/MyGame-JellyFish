using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletJellyHoming : MonoBehaviour
{
    Transform target; //���� ���� 
    public float bulletSpeed = 10f;
    private Rigidbody rb;
    [SerializeField] float damageEnemy = 3f; //�������� �����


    private LevelsSystem ShooterLevelSystem; //��� ������� �������


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Shark").transform; //���� ����� ������� � ����� ����� 
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 targetdirection = target.position - transform.position;
        transform.LookAt(target);
        rb.velocity = targetdirection.normalized * bulletSpeed;
    }

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
