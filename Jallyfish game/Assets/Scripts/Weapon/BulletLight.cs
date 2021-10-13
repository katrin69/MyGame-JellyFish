using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLight : MonoBehaviour
{
    private float bulletForce = 24f; //���� ����
    [SerializeField] private Rigidbody rb; //���� ����
    public int damage; //�������� �����
    public Vector3 dir;

    private float Timer; //������ ����� �������� ���� ��������
    public float defaultTime = 8f;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            return; //������ �� ������. ������� �� ������
        }


        gameObject.SetActive(false); //������� ������

        //���������� ��������� � ��� � ����� �����
        Debug.Log(other.transform.tag);


        if (other.gameObject.CompareTag("Shark"))
        {
            ////�������� ������ EnemyHealth � ������� ��������
            //EnemyHealth healthScript = other.transform.GetComponent<EnemyHealth>();

            //if (healthScript)
            //{
            //    healthScript.health -= damage;  //������ ���� �����

            //    if (healthScript.health < 0) //���� �� ����� ������ ����, �� ������ 0
            //    {
            //        healthScript.health = 0;
            //    }

            //}
            //else
            //{

            //    Debug.Log("No scripts");
            //}

            Destroy(other.gameObject,5f);//������� ������
        }
    }

}
