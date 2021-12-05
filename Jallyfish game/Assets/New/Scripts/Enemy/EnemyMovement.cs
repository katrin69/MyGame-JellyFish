 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // скорость врага
    public NavMeshAgent agent;

    protected Rigidbody rb;

    protected Vector3 TargetPosition;

    protected Vector3 StartPoint;
    protected Vector3 EndPoint;

    protected enum EMovement
    {
        Idle,
        StartToEnd,
        EndToStart,
    }

    protected EMovement CurrentMovement  = EMovement.Idle;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
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

    public virtual void SetPatrollingPoint(Vector3 newPoint)
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
