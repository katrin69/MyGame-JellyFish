using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : EnemyMovement
{
    public override void SetPatrollingPoint(Vector3 newPoint)
    {
        //EndPoint = newPoint;
        StartPoint = transform.position;
        SetTargetPosition(EndPoint);

        CurrentMovement = EMovement.StartToEnd;
    }
}
