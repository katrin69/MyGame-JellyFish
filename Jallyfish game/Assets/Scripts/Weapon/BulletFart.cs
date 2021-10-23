using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFart : MonoBehaviour
{
    [SerializeField] ParticleSystem _bulletFart;
    [SerializeField] float damageEnemy = 4f; //Величина урона
    private LevelsSystem ShooterLevelSystem; //брём систему уровней

    private void Start()
    {
        _bulletFart = GetComponent<ParticleSystem>();

    }

    private void Update()
    {
        Attack();
    }

    public void SetShooterLevelsSystem(LevelsSystem shooterLevelSystem) //система левлов на сохранение
    {
        ShooterLevelSystem = shooterLevelSystem;

    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _bulletFart.Play();
            Collider[] colliders = Physics.OverlapSphere(transform.position, 6f);
            foreach (Collider c in colliders)
            {
                if (c.gameObject.CompareTag("Shark"))
                {
                    ////Получаем скрипт здоровья акулы
                    EnemyHealth enemyHealthScript = c.transform.GetComponent<EnemyHealth>();
                    //передаём урон
                    enemyHealthScript.DeductHealth(damageEnemy, ShooterLevelSystem);
                }
            }
        }

    }
   

}
