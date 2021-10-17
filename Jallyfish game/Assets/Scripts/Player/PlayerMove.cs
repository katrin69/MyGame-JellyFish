using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMove : MonoBehaviour
{
    //��������
    public float moveSpeed = 15f; // �������� ��������   
    public float fastSpeed = 25f; // �������� ��� ����������
    private float realSpeed; // ������� �������� �������� . ���� ������� ���� �����������
    Vector3 movement; //���������� �����
   
    public Camera cam;
    public GameObject JellyfishModel; // �������� �������
    public Rigidbody rb; // 

    [Space]
    [Space]

    //������������
    [SerializeField] Slider sliderStanina; //������ ��� �������
    [SerializeField] float currentStamina;
    [SerializeField] float maxValueStamina;
    [SerializeField] float minValueStamina;
    //  [SerializeField] float staminaReturn = 4f;
    [Space]
    [Space]


    //�������
    public float rotationSpeed = 5f; // �������� ��������   
    public float distToGround; //��������� �� �����
    
    //��� � ���
    public Ray MouseRay;               // ���, ����� �������� �� ������� RayCast
    public LayerMask TerrainMask;       // ������ �� �������� �� ��������� ���, ����� �����

    private void Awake()
    {

        TerrainMask = LayerMask.GetMask("Terrain");     // ������� ������ �� ���� Terrain
    }

    private void Start()
    {
        realSpeed = moveSpeed;

        sliderStanina.maxValue = maxValueStamina;
        sliderStanina.minValue = minValueStamina;
        currentStamina = maxValueStamina;
    }


    void Update()
    {
        sliderStanina.value = currentStamina;
        GroundChexk();

        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        StaminaChecked();
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            realSpeed = fastSpeed; //����� ������ ������ �� �������� ����� �������
            currentStamina -=  Time.deltaTime * 10f; 
        }
        else
        {
            realSpeed = moveSpeed; //����� ������ �� ���� �� �������� ���������� �������
            currentStamina += Time.deltaTime /2f;
        }
    }

    //private void Stamina()
    //{
    //    if (maxValueStamina > 100f)
    //    {
    //        maxValueStamina = 100f; //����� ������ 100 �� ����������
    //    }
    //    staminaSlider.value = currentStamina;
    //}

    private void StaminaChecked()
    {
        if (currentStamina <= minValueStamina)
        {
            currentStamina = minValueStamina;
        }

        if(currentStamina >= maxValueStamina)
        {
            currentStamina = maxValueStamina;
        }
    }

   
}


