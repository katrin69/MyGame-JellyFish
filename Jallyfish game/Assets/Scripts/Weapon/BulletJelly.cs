using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletJelly : MonoBehaviour
{
    private float bulletForce = 24f; //скорость
    [SerializeField] private Rigidbody rb; //тело
    [SerializeField] float damageEnemy = 1f; //урон

    public Vector3 dir;



    private float Timer; //таймер исчезновения
    public float defaultTime = 8f;

    private LevelsSystem ShooterLevelSystem; //лэвлы

    private void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            gameObject.SetActive(false);
        }
    }
    //Никита любит лёшу

    private void OnEnable()
    {
        Timer = defaultTime;
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * bulletForce; //направление и скорость
    }

    public void SetShooterLevelsSystem(LevelsSystem shooterLevelSystem) //передаёт лэвлы
    {
        ShooterLevelSystem = shooterLevelSystem;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            return;
        }

        gameObject.SetActive(false);

        if (other.gameObject.CompareTag("Shark")) //если столкнулась с акулой
        {
            ////Ïîëó÷àåì ñêðèïò çäîðîâüÿ àêóëû
            EnemyHealth enemyHealthScript = other.transform.GetComponent<EnemyHealth>();
            //урон + опыт
            enemyHealthScript.DeductHealth(damageEnemy, ShooterLevelSystem);
        }
    }

}
