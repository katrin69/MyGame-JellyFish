using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : EnemyMovement
{
    private float speed = 0.4f; // скорость врага

    private float waitTime;
    private float startWaitTime;

    public Transform[] moveSpots;
    private int randomSpots;

    private void Start()
    {
        waitTime = startWaitTime;
        randomSpots = Random.Range(0, moveSpots.Length);
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpots].position, speed);
        
        if (Vector3.Distance(transform.position,moveSpots[randomSpots].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpots = Random.Range(0, moveSpots.Length);
            
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }

        }
    }

}
