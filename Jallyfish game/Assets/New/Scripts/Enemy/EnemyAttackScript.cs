using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public EnemyMovement EnemyMovementScript;

    public float Damage = 2f;

    private Transform target; //цель Медуза
    private float targetRange = 25f; //диапозон где ищем медузу

    //поиск Медузы
    private float SearchTimer = 0;
    private float SearchStep = 1;

    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>(); //ищем на акуле       
    }

    private void Update()
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
            animator.SetBool("Attack", true);
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

    public void FoundJellyfish()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, targetRange); //массив колайдеров вокруг

        Collider nearest = null; //рядом пока пусто с самого начала

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player")) //если в этом массиве есть медуза
            {
                FindObjectOfType<AudioManager>().Play("SoundSharkAttack2");

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
    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //отнимаем жизнь у медузы
            collision.gameObject.GetComponent<PlayerHealthScript>().RecountArmorp(-Damage);
        }
    }
}
