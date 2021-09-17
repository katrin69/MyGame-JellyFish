using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseMove : MonoBehaviour
{
    public Vector3 direction;
    public float acceleration;
    public Rigidbody rb;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.AddForce(direction.normalized * acceleration, ForceMode.Impulse);
        }
    }
}
