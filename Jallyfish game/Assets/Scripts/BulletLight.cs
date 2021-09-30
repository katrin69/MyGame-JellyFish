using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLight : MonoBehaviour
{
    private float bulletForce = 24f; //���� ����
    [SerializeField] private Rigidbody rb; //���� ����
    public int damage; //�������� �����
    public Vector3 dir;

    private float Timer; //������ ����� �������� ���� ��������
    public float defaultTime = 8f;

     //������ �� �������� ����
    public GameObject barObject;
    public HealthBar healthBarScript;

    private void Start()
    {
        barObject = GameObject.Find("Bar");//������� ���
        healthBarScript = barObject.GetComponent<HealthBar>(); //�������� ��� � ���������� �������
    }

    private void Update() // ����� ����� �������� ������ ��������
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            gameObject.SetActive(false);
        }
    }
    //������ ����� ˸��

    private void OnEnable()
    {
        Timer = defaultTime;
    }

    private void FixedUpdate()
    {
       rb.velocity = Vector3.forward * bulletForce; //�������� ������
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false); //������� ������

        //���������� ��������� � ��� � ����� �����
        Debug.Log(collision.transform.tag);

        if (collision.gameObject.CompareTag("Shark"))
        {
            //�������� ������ EnemyHealth � ������� ��������
            EnemyHealth healthScript = collision.transform.GetComponent<EnemyHealth>();

            if (healthScript && healthBarScript)
            {
                healthScript.health -= damage;  //������ ���� �����

                if (healthScript.health < 0) //���� �� ����� ������ ����, �� ������ 0
                {
                    healthScript.health = 0;
                }
                //���������� � ��� ��������� �� �� � �� ������������ �����
                healthBarScript.health = healthScript.health;
                healthBarScript.healthMax = healthScript.healthMax;
                //����������� ���
                healthBarScript.showBar = true;
            }
            else
            {

                Debug.Log("No scripts");
            }
            Destroy(collision.gameObject);//������� ������
        }
        
    }
}
