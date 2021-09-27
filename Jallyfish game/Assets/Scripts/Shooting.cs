using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 20f;

    //Никита любит Лёшу

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Shoot();
        }


        //  if (Input.GetMouseButton(1))
        //  {
        //      GameObject newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        //     newBullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletForce;
        //  }

    }

    void Shoot()
    {
        // GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Rigidbody rb = bullet.GetComponent<Rigidbody>();
        // rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);

        GameObject bullet = ObjectPooler.instance.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.SetActive(true);
        }
    }
}
