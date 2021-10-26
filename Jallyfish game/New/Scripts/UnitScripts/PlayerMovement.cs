using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //будет отвечать за все передвижение

    //ñêîðîñòü
    private float moveSpeed = 15f; // Ñêîðîñòü äâèæåíèÿ   
    private float fastSpeed = 25f; // Ñêîðîñòü ïðè óñêîðååíèè
    private float realSpeed; // Òåêóùåå çíà÷åíèå ñêîðîñòè . Ëèáî îáû÷íàÿ ëèáî óâåëè÷åííàÿ

    private Rigidbody rb; // 

    private Vector3 HorizontalMovement;
    private float HorizontalMovementAcceleration = 0.1f;

    private Quaternion LookRotation = Quaternion.identity;
    private float rotationSpeed = 5f; // Ñêîðîñòü ïîâîðîòà  

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        realSpeed = moveSpeed; //çàäà¸ì ñêîðîñòü
    }

    private void Update()
    {   //направление со скоростью
        rb.velocity = Vector3.Lerp(rb.velocity, HorizontalMovement.normalized * realSpeed, HorizontalMovementAcceleration);
        transform.rotation = Quaternion.Lerp(transform.rotation, LookRotation, Time.deltaTime * rotationSpeed);
    }

    public void SetVerticalPosition(float offset)
    {
        Vector3 position = transform.position;
        position.y = offset;
        transform.position = position;
    }

    public void ChangeMovementDirection(Vector3 direction)
    {   //мы задаём направление
        HorizontalMovement += direction;

        if (HorizontalMovement != Vector3.zero)
        {
            ChangeLookingPoint(HorizontalMovement);
        }
    }

    public void ChangeLookingPoint(Vector3 point)
    {
        LookRotation = Quaternion.LookRotation(point); //ðàñù¸ò ïîâîðà öåëè
        LookRotation.x = 0; // ÷òîáû ìû íå ïîâåðíóëè åå íåïî òîé îñè
        LookRotation.z = 0;        
    }
}
