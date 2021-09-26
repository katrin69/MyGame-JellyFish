using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLight : MonoBehaviour
{
    private float bulletForce = 24f;
    [SerializeField] private Rigidbody rb;
    public float Timer = 3;
   // public Transform firePoint;

    private void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.forward * bulletForce;
        //rb.AddForce(Vector3.forward * bulletForce);
        // rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shark"))
        {
            gameObject.SetActive(false);
        }
        
    }
}
