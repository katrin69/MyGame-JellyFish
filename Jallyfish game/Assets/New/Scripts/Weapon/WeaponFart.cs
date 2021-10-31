using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFart : MonoBehaviour
{
    //большой пук урон 4

    [SerializeField] ParticleSystem _bulletFart;
    private float damageEnemyFart = 4f; //урон

    private LevelsSystem ShooterLevelSystem; //лэвлы

    //таймер
    private float Timer;
    public float defaultTime = 2f;

    private void Awake()
    {
        defaultTime = _bulletFart.main.duration;
    }

    private void OnEnable()
    {
        Timer = defaultTime;
        ShootFart();
    }

    private void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            this.ReturnToPool();
        }
    }

    public void SetShooterLevelsSystem(LevelsSystem shooterLevelSystem) //передаёт лэвлы
    {
        ShooterLevelSystem = shooterLevelSystem;
    }

    public void ShootFart()
    {
        _bulletFart.Play();

        Collider[] colliders = Physics.OverlapSphere(transform.position, 6f);

        foreach (Collider c in colliders)
        {
            if (c.gameObject.CompareTag("Shark"))
            {
                if (ShooterLevelSystem != null)
                {
                    //скрипт врагов
                    EnemyHealth enemyHealthScript = c.transform.GetComponent<EnemyHealth>();
                    //урон + опыт
                    enemyHealthScript.DeductHealth(damageEnemyFart, ShooterLevelSystem);

                    this.ReturnToPool();
                }
            }
        }
    }
}
