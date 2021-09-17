using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBMovePosition : MonoBehaviour
{
    public float forceMult = 200;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }
    void Update()
    {
        Vector3 newPosition = (transform.position + (transform.forward * Time.deltaTime));
        rb.MovePosition(newPosition);
    }
}
