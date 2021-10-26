using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; //������
    public Transform firePoint; //����� �� ���� ��������
    public float bulletForce = 20f; //�������� �������� ������
                                    

    LevelsSystem levelSystem;

    //��� �������� ����
    [SerializeField] ParticleSystem _bulletFart;
    [SerializeField] float damageEnemyFart = 4f; //�������� �����


    private void Start()
    {
       levelSystem = GetComponent<LevelsSystem>(); // ������ ���� ����� ��������� ���

    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) //���� ������ ������ �� ��������
        {

            ShootBulletHoming();
            // ShootBulletLight();
            // ShootBulletJelly();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShootFart();
        }


    }

    void ShootBulletLight() //������� ��������
    {
        GameObject bullet = ObjectPooler.instance.GetPooledObject(); //������ ���� �� ����

        if (bullet != null) //���� ���� �� ����� �� 
        {

            if (levelSystem != null) //���� ��� ���� �� ���� �� 
            {
                BulletLight bulletLight = bullet.GetComponent<BulletLight>(); //������ ����
                bulletLight.SetShooterLevelsSystem(levelSystem);  //����� ������� ����� �� ���������� ����� ��������� ����
            }

            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }


    void ShootBulletJelly() //������� ��������
    {
        GameObject bullet = ObjectPooler.instance.GetPooledObject(); //������ ���� �� ����

        if (levelSystem != null)
        {
            BulletJelly bulletJelly = bullet.GetComponent<BulletJelly>(); //������ ����
            bulletJelly.SetShooterLevelsSystem(levelSystem);  //����� ������� ����� �� ���������� ����� ��������� ����
        }
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetActive(true);
    }


    void ShootBulletHoming() //���������������
    {
        GameObject bullet = ObjectPooler.instance.GetPooledObject(); //������ ���� �� ����

        if (bullet != null) //���� ���� �� ����� �� 
        {

            if (levelSystem != null) //���� ��� ���� �� ���� �� 
            {
                BulletJellyHoming bulletLight = bullet.GetComponent<BulletJellyHoming>(); //������ ����
                bulletLight.Initialize(transform, levelSystem);  //����� ������� ����� �� ���������� ����� ��������� ����
            }

            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }


    public void ShootFart() //������� ���
    {
        _bulletFart.Play();

        Collider[] colliders = Physics.OverlapSphere(transform.position, 6f);
        foreach (Collider c in colliders)
        {
            if (c.gameObject.CompareTag("Shark"))
            {
                if (levelSystem != null)
                {
                    ////�������� ������ �������� �����
                    EnemyHealth enemyHealthScript = c.transform.GetComponent<EnemyHealth>();
                    //������� ����
                    enemyHealthScript.DeductHealth(damageEnemyFart, levelSystem);

                }
            }
        }
    }
}
