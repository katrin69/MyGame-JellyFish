using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFart : MonoBehaviour
{
    [SerializeField] ParticleSystem _bulletFart;


    private void Start()
    {
        _bulletFart = GetComponent<ParticleSystem>();
        
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha3)) //если нажата кнопка то стрелять
        {
            Shoot();
        }

    }

    public void Shoot()
    {
        _bulletFart.Play();
    }
}
