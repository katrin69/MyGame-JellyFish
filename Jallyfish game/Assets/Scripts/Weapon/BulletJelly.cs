using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletJelly : MonoBehaviour
{
    private float bulletForce = 24f; //���� ����
    [SerializeField] private Rigidbody rb; //���� ����
    [SerializeField] float damageEnemy = 1f; //�������� �����

    public Vector3 dir;



    private float Timer; //������ ����� �������� ���� ��������
    public float defaultTime = 8f;

    private LevelsSystem ShooterLevelSystem; //��� ������� �������

    private void Update() // ����� ����� �������� ������ ��������
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            gameObject.SetActive(false);
        }
    }
    //������ ����� ˸��

    private void OnEnable()
    {
        Timer = defaultTime;
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * bulletForce; //�������� ������
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

        //���������� ��������� � ��� � ����� �����
        Debug.Log(other.transform.tag);


        if (other.gameObject.CompareTag("Shark")) //���� ���� ����������� � ������
        {
            ////�������� ������ �������� �����
            EnemyHealth enemyHealthScript = other.transform.GetComponent<EnemyHealth>();
            //������� ����
            enemyHealthScript.DeductHealth(damageEnemy, ShooterLevelSystem);
        }
    }

}
