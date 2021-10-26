using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public float distToGround = 5f; //��������� �� �����

    private LayerMask TerrainMask;// ������ �� �������� �� ��������� ���, ����� �����

    private void Awake()
    {
        TerrainMask = LayerMask.GetMask("Terrain");
    }

    public float CheckGround()
    {
        Ray isGround = new Ray(transform.position, Vector3.down);
        float verticalPosition = 0;

        if (Physics.Raycast(isGround, out var hitInfo, distToGround * 10f, TerrainMask)) //���,�����,��������� �� �����,�����
        {
            Debug.DrawRay(isGround.origin, isGround.direction * distToGround, Color.green); //���������� ���
            //������ ����� ��������� ������� . ����� ��� ��� ��������� ����� y + ��������� �� ����� ����� ������� �� y

            verticalPosition = hitInfo.point.y + distToGround;
        }

        return verticalPosition;
    }
}
