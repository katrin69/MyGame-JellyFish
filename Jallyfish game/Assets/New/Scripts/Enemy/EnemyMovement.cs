using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // скорость врага

    private Rigidbody rb;

    private Vector3 TargetPosition;

    private Vector3 StartPoint;
    private Vector3 EndPoint;

    private enum EMovement
    {
        Idle,
        StartToEnd,
        EndToStart,
    }

    private EMovement CurrentMovement  = EMovement.Idle;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (CurrentMovement != EMovement.Idle)
        {
            if ((TargetPosition - transform.position).magnitude < 0.5f)
            {
                switch (CurrentMovement)
                {
                    case EMovement.StartToEnd:
                        TargetPosition = StartPoint;
                        CurrentMovement = EMovement.EndToStart;
                        break;
                    case EMovement.EndToStart:
                        TargetPosition = EndPoint;
                        CurrentMovement = EMovement.StartToEnd;
                        break;
                }
            }

            rb.velocity = (TargetPosition - transform.position).normalized * moveSpeed;
            transform.LookAt(TargetPosition);
        }
    }

    public void Stop()
    {
        rb.velocity = Vector3.zero;
        CurrentMovement = EMovement.Idle;
    }

    public void SetPatrollingPoint(Vector3 newPoint)
    {
        EndPoint = newPoint;
        StartPoint = transform.position;
        SetTargetPosition(EndPoint);

        CurrentMovement = EMovement.StartToEnd;
    }

    public void SetTargetPosition(Vector3 newPositioin)
    {
        TargetPosition = newPositioin;
    }
}
