using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFart : MonoBehaviour
{
    //большой пук урон 4

    [SerializeField] ParticleSystem _bulletFart;
    private float damageEnemyFart = 4f; //урон

    private PlayerLevelSystem ShooterLevelSystem; //лэвлы

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
            ResourceManager.ReturnToPool(gameObject);
        }
    }

    public void SetShooterLevelsSystem(PlayerLevelSystem shooterLevelSystem) //передаёт лэвлы
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
                    _bulletFart.Play();
                    //скрипт врагов
                    EnemyHealthScript enemyHealthScript = c.transform.GetComponent<EnemyHealthScript>();
                    //урон + опыт
                    enemyHealthScript.DeductHealth(damageEnemyFart * ShooterLevelSystem.CurrentLevel, ShooterLevelSystem);                   
                }
            }
        }

        StartCoroutine(DelayedPoolReturn());
    }

    private IEnumerator DelayedPoolReturn()
    {
        yield return new WaitForSeconds(_bulletFart.main.duration);
        ResourceManager.ReturnToPool(gameObject);
    }
}
