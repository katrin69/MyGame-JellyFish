using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public EnemyMovement EnemyMovementScript;

    public float Damage = 2f;

    protected Transform target; //цель Медуза
    public float targetRange = 25f; //диапозон где ищем медузу

    //поиск Медузы
    protected float SearchTimer = 0;
    protected float SearchStep = 1;

    protected Animator Animator;

    protected AudioManager AudioManager;
    protected ResourceManager ResourceManager;

    private void Awake()
    {
        Animator = GetComponent<Animator>(); //ищем на акуле       
    }

    public virtual void Update()
    {
        //bool isAttack = animator.GetBool("isAttack");
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    animator.SetBool("isAttack", true);
        //}
        //if (!Input.GetKeyDown(KeyCode.Space))
        //{
        //    animator.SetBool("isAttack", false);
        //}

        if (target != null) //если цель не пуста то идём на цель
        { 
            Animator.SetBool("Attack", true);
            EnemyMovementScript.SetTargetPosition(target.position);
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

    public void Init(ResourceManager resourceManager, AudioManager audioManager)
    {
        ResourceManager = resourceManager;
        AudioManager = audioManager;
    }

    protected void FoundJellyfish()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, targetRange); //массив колайдеров вокруг

        Collider nearest = null; //рядом пока пусто с самого начала

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player")) //если в этом массиве есть медуза
            {
                AudioManager.Play("SoundSharkAttack2");

                if (nearest == null) //если не очень близко то пофиг
                {
                    nearest = collider;
                }
                else
                {
                    if ((collider.transform.position - transform.position).magnitude < (nearest.transform.position - transform.position).magnitude) //если близко то ударить
                    {
                        nearest = collider;
                    }
                }
            }
        }

        if (nearest != null) //цель станогвится ближе
        {
            target = nearest.transform;
        }
    }

    //если сталкиваемся с медузой
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //отнимаем жизнь у медузы
            collision.gameObject.GetComponent<PlayerHealthScript>().RecountArmorp(-Damage * Time.deltaTime); 
        }
    }
}
