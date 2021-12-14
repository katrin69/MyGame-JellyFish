using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackScript : EnemyAttackScript
{
    public float AttackTimer = 5;
    public float AttackCooldown = 5;

    public override void Update()
    {
        if (target != null) //если цель не пуста то идём на цель
        {
            Attack();
        }
        else
        {
            SearchTimer += Time.deltaTime;

            if (SearchTimer >= SearchStep) //при этом ищем медузу каждую секунду
            {
                SearchTimer = 0;
                FoundJellyfish(); //нашёл медузу
            }
        }
    }

    private void Attack()
    {
        AttackTimer += Time.deltaTime;

        if (AttackTimer >= AttackCooldown)
        {
            AttackTimer = 0;

            EnemyMovementScript.Stop();

            transform.LookAt(target);

            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        ShootWeaponForBoss();

        yield return new WaitForSeconds(1f);

        EnemyMovementScript.Resume();
    }

    //стреляем пулей
    private void ShootWeaponForBoss()
    {
        GameObject bullet = ResourceManager.GetObjectInstance(EObjectType.WeaponForBoss); //берём пулю из пула

        if (bullet != null) //если пуля не пуста
        {
            WeaponForBoss weaponForBoss = bullet.GetComponent<WeaponForBoss>(); //то берём скрипт пули
            weaponForBoss.target = target;

            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }
}
