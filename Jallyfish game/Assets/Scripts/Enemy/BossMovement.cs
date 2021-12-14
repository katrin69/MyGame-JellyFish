using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : EnemyMovement
{
    public Transform[] moveSpots;
    private int spotIndex;

    private void Start()
    {
        SwitchTargets();
    }

    public override void SwitchTargets()
    {
        spotIndex = Random.Range(0, moveSpots.Length);
        SetPatrollingPoint(moveSpots[spotIndex].position);
    }
}
