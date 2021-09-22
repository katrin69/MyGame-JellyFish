using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� ��������   

    public Rigidbody rb; // 

    public Camera cam;

    public GameObject JellyfishModel; // �������� �������

    Vector3 movement;

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

        //�������� �� �����
        if (Vector3.Angle(Vector3.forward,movement)>1f|| Vector3.Angle(Vector3.forward,movement) ==0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, movement, moveSpeed, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0)) // ���������, ������ �� ����� ������ �����
        {
            MouseRay = cam.ScreenPointToRay(Input.mousePosition); // ����� ��� �� ������ ������������ ������� �����

            int results = Physics.RaycastNonAlloc(MouseRay, CastResults, 500, TerrainMask); // ������ ���� �� ����

            if (results > 0)
            {
                TargetPosition = CastResults[0].point + Vector3.up * Height; // ���� ����������� >1 �� � �������� ������� ���������� �����, ���� ����� ��������
            }
        }

        Vector3 targetDirection = TargetPosition - transform.position; // ������� ����������� �� ���� �� �������� �����

        if (targetDirection.magnitude > 0.5f) // ���� ���������� ������ 0.5
        {
            rb.velocity = targetDirection.normalized * moveSpeed; // �� ������ ������� �������� �� ����������� � �������� �����
        }
        else
        {
            rb.velocity = ZeroVelocity; // ���� �� ����� � ������� ������ - ���������� �������� � 0
        }

        VerticalOffset = Mathf.Sin(Time.realtimeSinceStartup) / VerticalDumpening; // ������� ������������ ���������� �� ������ �� �������, �������� ������� ��������� �������� �� -1 �� 1

        //PositionOffset = JellyfishModel.transform.position; // ����� ������� ������ �������
        //PositionOffset.y += VerticalOffset; // ��������� �� ������� ������������ ����������
        //JellyfishModel.transform.position = PositionOffset; // ���������� ���������� ������� � ������� - �������� ������� ��������� �����-����

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        //rb.transform.LookAt(mousePos);
        //Vector3 lookDir = mousePos - rb.position;
        // transform.LookAt(transform.position + lookDir);
    }

}
