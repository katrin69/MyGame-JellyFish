using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFart : MonoBehaviour, IWeaponaBase
{
    //большой пук урон 4

    [SerializeField] ParticleSystem _bulletFart;
    private float damageEnemyFart = 4f; //урон

    public void Shoot()
    {
        _bulletFart.Play();

        Collider[] colliders = Physics.OverlapSphere(transform.position, 6f);
        foreach (Collider c in colliders)
        {
            if (c.gameObject.CompareTag("Shark"))
            {
                //урон врагу
            }
        }
    }

}
