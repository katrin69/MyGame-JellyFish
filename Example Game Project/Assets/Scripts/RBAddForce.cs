using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBAddForce : MonoBehaviour
{
    public float forceMult = 200;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rb.AddForce(transform.forward * forceMult * Time.deltaTime); 
    }
}
