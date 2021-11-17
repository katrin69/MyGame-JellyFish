using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackScript : EnemyAttackScript
{
    private float dis;
    public float howClose;

    private Transform player;
    Transform target; //цель Медуза

    //поиск Медузы
    private float SearchTimer = 0;
    private float SearchStep = 1;

    protected AudioManager AudioManager;
    private ResourceManager ResourceManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (target != null) //если цель не пуста то идём на цель
        {
            transform.LookAt(target);
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

    public void SetAudioManager(AudioManager audioManager)
    {
        AudioManager = audioManager;
    }

    public void FoundJellyfish()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 50f); //массив колайдеров вокруг

        Collider nearest = null; //рядом пока пусто с самого начала

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player")) //если в этом массиве есть медуза
            {
                if (nearest == null) //если не очень близко то пофиг
                {
                    nearest = collider;
                    ShootWeaponForBoss();

                }
                else
                {
                    //что то делать
                }
            }
        }

        if (nearest != null) //цель станогвится ближе
        {
            target = nearest.transform;
        }
    }


    //стреляем пулей
    private void ShootWeaponForBoss()
    {
        GameObject bullet = ResourceManager.GetObjectInstance(EObjectType.WeaponForBoss); //берём пулю из пула

        if (bullet != null) //если пуля не пуста
        {

            WeaponForBoss weaponForBoss = bullet.GetComponent<WeaponForBoss>(); //то берём скрипт пули

            bullet.transform.position = target.position;
            bullet.transform.rotation = target.rotation;
            bullet.SetActive(true);
        }
    }
}
