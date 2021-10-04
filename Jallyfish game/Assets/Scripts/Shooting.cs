using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; //молния
    public Transform firePoint; //место от куда стрелять
    public float bulletForce = 20f; //скорость движения молнии


    //Никита любит Лёшу

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) //если нажата кнопка то стрелять
        {
            Shoot();
        }


        //  if (Input.GetMouseButton(1))
        //  {
        //      GameObject newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        //     newBullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletForce;
        //  }

    }

    void Shoot() //метод стрельбы
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
