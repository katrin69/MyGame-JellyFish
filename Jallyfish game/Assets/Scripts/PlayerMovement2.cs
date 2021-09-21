using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody rb;
    public Camera cam;

    Vector3 movement;
    Vector3 mousePos;


    
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");

       mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        rb.transform.LookAt(mousePos);
        //Vector3 lookDir = mousePos - rb.position;
        // transform.LookAt(transform.position + lookDir);

    }
        
}
