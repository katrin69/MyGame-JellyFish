using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 15f; // �������� ��������   
    public float fastSpeed = 25f; // �������� ��� ����������   
    private float realSpeed; // ������� �������� �������� . ���� ������� ���� �����������


    public float rotationSpeed = 5f; // �������� ��������   
    public float distToGround; //��������� �� �����


    public Rigidbody rb; // 

    public Camera cam;

    public GameObject JellyfishModel; // �������� �������

    Vector3 movement; //���������� �����

    public Ray MouseRay;               // ���, ����� �������� �� ������� RayCast

    public LayerMask TerrainMask;       // ������ �� �������� �� ��������� ���, ����� �����

    private void Awake()
    {

        TerrainMask = LayerMask.GetMask("Terrain");     // ������� ������ �� ���� Terrain
    }

    private void Start()
    {
        realSpeed = moveSpeed;
    }


    void Update()
    {
        GroundChexk();

        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        FastSpeed(); //����� ��� ���������

        //������� ������ �� �����
        MouseRay = cam.ScreenPointToRay(Input.mousePosition); //������� ��� ������� ����� ������������ ����� ������
        float hitDist = 0.0f; //���������� ��������� �� �����
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        Debug.DrawRay(transform.position, transform.forward * 30f, Color.red);

        if (playerPlane.Raycast(MouseRay, out hitDist) && Input.GetMouseButton(0))
        {
            Vector3 targetPoint = MouseRay.GetPoint(hitDist); //���������� ����� � �������� ��������� ����� ����. hitDist ���������� ���������
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position); //������ ������ ����
            targetRotation.x = 0; // ����� �� �� ��������� �� ���� ��� ���
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);
        }

        //������������� �� ����������� ��������
        if (movement.magnitude > Mathf.Abs(0.5f))
        {
            Quaternion lookRotation = Quaternion.LookRotation(movement);//������ ������ ����

            lookRotation.x = 0; // ����� �� �� ��������� �� ���� ��� ���
            lookRotation.z = 0;

            transform.rotation = Quaternion.Lerp(transform.rotation/*��������� �����*/,
                                 lookRotation/*���� ����� �������� */,
                                 Time.deltaTime * rotationSpeed);

        }

        //������
        rb.MovePosition(rb.position + movement * realSpeed * Time.fixedDeltaTime); //���������
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sea"))
        {
           
            print("���� ���");

        }
    }


    void GroundChexk()
    {
        Ray isGround = new Ray(transform.position, Vector3.down);


        if (Physics.Raycast(isGround, out var hitInfo, distToGround * 10f, TerrainMask)) //���,�����,��������� �� �����,�����
        {

            Debug.DrawRay(isGround.origin, isGround.direction * distToGround, Color.green); //���������� ���
            //������ ����� ��������� ������� . ����� ��� ��� ��������� ����� y + ��������� �� ����� ����� ������� �� y
            var dif = (hitInfo.point.y + distToGround) - transform.position.y;
            //����������� ����� ����� ����� �������� �� �������
            movement = Vector3.up * dif;

        }

    }

    void FastSpeed() //����� ������� ����������� ��������
    {
        PlayerFastSpeedBar.instance.SetValue( moveSpeed/(float)realSpeed);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            realSpeed = fastSpeed;
        }
        else
        {
            realSpeed = moveSpeed;
        }
    }
}


