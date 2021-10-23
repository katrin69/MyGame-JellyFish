using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFart : MonoBehaviour
{
    [SerializeField] ParticleSystem _bulletFart;
    [SerializeField] float damageEnemy = 4f; //�������� �����
    private LevelsSystem ShooterLevelSystem; //��� ������� �������

    private void Start()
    {
        _bulletFart = GetComponent<ParticleSystem>();

    }

    private void Update()
    {
        Attack();
    }

    public void SetShooterLevelsSystem(LevelsSystem shooterLevelSystem) //������� ������ �� ����������
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
                    ////�������� ������ �������� �����
                    EnemyHealth enemyHealthScript = c.transform.GetComponent<EnemyHealth>();
                    //������� ����
                    enemyHealthScript.DeductHealth(damageEnemy, ShooterLevelSystem);
                }
            }
        }

    }
   

}
