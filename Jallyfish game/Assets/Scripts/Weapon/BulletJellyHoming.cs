using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletJellyHoming : MonoBehaviour
{
    public Transform target; //���� ���� 
    public float bulletSpeed = 5f;
    private Rigidbody rb;

    private void Start()
    {
       // target = GameObject.FindGameObjectWithTag("Shark").transform; //���� ����� ������� � ����� ����� 
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * bulletSpeed;
    }
}
