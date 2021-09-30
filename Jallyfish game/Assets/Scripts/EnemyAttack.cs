using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
   // public Transform player; //���� ������
    public float moveSpeed = 10f; // �������� ��������
    private Rigidbody rb;



    private float dis; //���������� ����� ������ � �������
    public float howClose; //��������� ����� ������ � ������
    private Transform player;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //dis = Vector3.Distance(player.position, transform.position);
        Vector3 targetdirection = player.position - transform.position;
        if (targetdirection.magnitude > 0.5f) // ���� ���������� ������ 0.5
        {
            transform.LookAt(player);
            rb.velocity = targetdirection.normalized * moveSpeed; // �� ������ ������� �������� �� ����������� � �������� �����
        }


        if (dis <= howClose)
        {
            transform.LookAt(player);
            GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed);
        }

    }

    //���� ����� ������������ � �������
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //���������� � ������
            collision.gameObject.GetComponent<PlayerHealth>().RecountHp(-1);
            print("������ ����� ˸��");
        }
    }



}
