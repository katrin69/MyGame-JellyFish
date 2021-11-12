using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTerrorAttackScript : EnemyAttackScript
{
    public GameObject shark;
    public ParticleSystem _explosion;
    public Collider SharkCollider;

    private void OnEnable()
    {
        shark.SetActive(true);
        SharkCollider.enabled = true;
    }
   
    //если сталкиваемся с медузой
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("SoundExplosion1");

            _explosion.Play();
            //отнимаем жизнь у медузы
            collision.gameObject.GetComponent<PlayerHealthScript>().RecountArmorp(Damage);

            StartCoroutine(DelayedPoolReturn());
        }
    }

    private IEnumerator DelayedPoolReturn()
    {
        shark.SetActive(false);
        SharkCollider.enabled = false;
        yield return new WaitForSeconds(_explosion.main.duration);
        ResourceManager.ReturnToPool(gameObject);
    }
}
