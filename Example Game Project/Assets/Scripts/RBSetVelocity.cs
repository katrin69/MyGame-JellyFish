using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBSetVelocity : MonoBehaviour
{
    public float forceMult = 200;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * Time.deltaTime * forceMult;
    }
    void Update()
    {
        
    }
}
