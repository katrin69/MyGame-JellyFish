using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� ��������   

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

        MouseRay = cam.ScreenPointToRay(Input.mousePosition); //������� ��� ������� ����� ������������ ����� ������
        float hitDist = 0.0f;
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        if (playerPlane.Raycast(MouseRay,out hitDist))
        {
            Vector3 targetPoint = MouseRay.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);

        }


        ////�������� �� �����
        //if (Vector3.Angle(Vector3.forward, movement) > 1f || Vector3.Angle(Vector3.forward, movement) == 0)
        //{
        //    Vector3 direct = Vector3.RotateTowards(transform.forward, movement, moveSpeed, 0.0f);
        //    transform.rotation = Quaternion.LookRotation(direct);
        //}
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); //���������

        Vector3 targetDirection = mousePos - transform.position; // ������� ����������� �� ���� �� �������� �����
        float angle = Mathf.Atan2(targetDirection.z, targetDirection.x) * Mathf.Rad2Deg-90 ;
        transform.eulerAngles = new Vector3(transform.rotation.x,  angle, transform.rotation.z);


        //if (Input.GetMouseButton(0)) // ���������, ������ �� ����� ������ �����
        //{
        //    MouseRay = cam.ScreenPointToRay(Input.mousePosition); // ����� ��� �� ������ ������������ ������� �����

        //    int results = Physics.RaycastNonAlloc(MouseRay, CastResults, 500, TerrainMask); // ������ ���� �� ����

        //    if (results > 0)
        //    {
        //        TargetPosition = CastResults[0].point + Vector3.up * Height; // ���� ����������� >1 �� � �������� ������� ���������� �����, ���� ����� ��������
        //    }

        //    Vector3 targetDirection = TargetPosition - transform.position; // ������� ����������� �� ���� �� �������� �����

        //    var angle = Mathf.Atan2(targetDirection.z, targetDirection.x) * Mathf.Rad2Deg;// ������� ����
        //    transform.eulerAngles = new Vector3(transform.rotation.x, -angle, transform.rotation.z); // ����� ����
        //}



        //VerticalOffset = Mathf.Sin(Time.realtimeSinceStartup) / VerticalDumpening; // ������� ������������ ���������� �� ������ �� �������, �������� ������� ��������� �������� �� -1 �� 1

        //PositionOffset = JellyfishModel.transform.position; // ����� ������� ������ �������
        //PositionOffset.y += VerticalOffset; // ��������� �� ������� ������������ ����������
        //JellyfishModel.transform.position = PositionOffset; // ���������� ���������� ������� � ������� - �������� ������� ��������� �����-����




    }

}
