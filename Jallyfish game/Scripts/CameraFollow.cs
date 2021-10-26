using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //������ �� �� ���� �� ����� ���������,�����
    public Vector3 offset; //����������� ������� ������ �� ��� ����

    public float smoothSpeed = 0.125f; //��� ������ ������ ����� ����������� � ����,������������ ���������

    private void FixedUpdate()
    {
        Vector3 desirePosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desirePosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
