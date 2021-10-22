using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; //������
    public Transform firePoint; //����� �� ���� ��������
    public float bulletForce = 20f; //�������� �������� ������
    //������ ����� ˸��

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) //���� ������ ������ �� ��������
        {
            Shoot();
        }

    }

    void Shoot() //����� ��������
    {
        GameObject bullet = ObjectPooler.instance.GetPooledObject(); //������ ���� �� ����

        if (bullet != null) //���� ���� �� ����� �� 
        {
            LevelsSystem levelSystem = GetComponent<LevelsSystem>(); // ������ ���� ����� ��������� ���

            if (levelSystem != null) //���� ��� ���� �� ���� �� 
            {
                BulletLight bulletLight = bullet.GetComponent<BulletLight>(); //������ ����
                bulletLight.SetShooterLevelsSystem(levelSystem);  //����� ������� ����� �� ���������� ����� ��������� ����
            }
            else
            {
                BulletJelly bulletLight = bullet.GetComponent<BulletJelly>(); //������ ����
                bulletLight.SetShooterLevelsSystem(levelSystem);  //����� ������� ����� �� ���������� ����� ��������� ����
            }

            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }
}
