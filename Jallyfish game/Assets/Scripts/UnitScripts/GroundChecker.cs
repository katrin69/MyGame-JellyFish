using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public float distToGround = 5f; //мето до земли

    private LayerMask TerrainMask;//наш песок

    private void Awake()
    {
        TerrainMask = LayerMask.GetMask("Terrain");
    }

    public float CheckGround()
    {
        Ray isGround = new Ray(transform.position, Vector3.down);
        float verticalPosition = 0;

        if (Physics.Raycast(isGround, out var hitInfo, distToGround * 10f, TerrainMask)) //луч до песка
        {
            Debug.DrawRay(isGround.origin, isGround.direction * distToGround, Color.green); //рисует луч

            verticalPosition = hitInfo.point.y + distToGround;
        }

        return verticalPosition;
    }
}
