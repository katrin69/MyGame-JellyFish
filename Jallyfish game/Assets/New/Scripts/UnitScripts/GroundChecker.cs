using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public float distToGround = 5f; //Дистанция до земли

    private LayerMask TerrainMask;// Фильтр по которому мы отсеиваем все, кроме песка

    private void Awake()
    {
        TerrainMask = LayerMask.GetMask("Terrain");
    }

    public float CheckGround()
    {
        Ray isGround = new Ray(transform.position, Vector3.down);
        float verticalPosition = 0;

        if (Physics.Raycast(isGround, out var hitInfo, distToGround * 10f, TerrainMask)) //луч,точка,дистанция до земли,земля
        {
            Debug.DrawRay(isGround.origin, isGround.direction * distToGround, Color.green); //показывает луч
            //создаём новую переменую Разница . Точка где луч пересекат землю y + дистанция до земли минус позиция по y

            verticalPosition = hitInfo.point.y + distToGround;
        }

        return verticalPosition;
    }
}
