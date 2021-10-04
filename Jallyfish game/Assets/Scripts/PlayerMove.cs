using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� ��������   
    public float rotationSpeed = 5f; // �������� ��������   


    public Rigidbody rb; // 

    public Camera cam;

    public GameObject JellyfishModel; // �������� �������

    Vector3 movement; //���������� �����
    Vector3 mousePos;

    public Ray MouseRay;               // ���, ����� �������� �� ������� RayCast
    public RaycastHit[] CastResults;   // ���������� RayCast

    public LayerMask TerrainMask;       // ������ �� �������� �� ��������� ���, ����� �����

    [Space(10)]
    public float VerticalOffset;        // ������������ ���������� �� ������� �������

    [Range(1, 100)]
    public float VerticalDumpening = 10;  // ����������� ���������� ������������ ���������

    public Vector3 PositionOffset;      // ����� ���������� �� ������� �������

    public Vector3 TargetPosition;      // �������, � ������� ����� ������

    public Vector3 ZeroVelocity = Vector3.zero;  // ���� ���

    public float Height;    // �������� ������ ������� ��� ������

    private void Awake()
    {
        CastResults = new RaycastHit[1];       // ������� ������ � ����� �����������
        TerrainMask = LayerMask.GetMask("Terrain");     // ������� ������ �� ���� Terrain
    }

    private void Start()
    {                       // ���� ������� - ������ ���� - ��������� - ��������� ����� - ������ 
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 10, TerrainMask)) // ������ ���� ��� ����
        {
            Height = (hit.point - transform.position).magnitude; // ���������� ���������� �� ������� �� �����
        }

        TargetPosition = transform.position; // ���������� ������� ������� ��� ��������
    }

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
       
        //������� ������ �� �����
        MouseRay = cam.ScreenPointToRay(Input.mousePosition); //������� ��� ������� ����� ������������ ����� ������
        float hitDist = 0.0f; //���������� ��������� �� �����
        Plane playerPlane = new Plane(Vector3.up, transform.position);

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
            transform.rotation = Quaternion.Lerp(transform.rotation/*��������� �����*/, 
                                 Quaternion.LookRotation(movement)/*���� ����� �������� */,
                                 Time.deltaTime * rotationSpeed);

        }
      
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); //���������

    }

}
