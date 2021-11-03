using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerMovement : MonoBehaviour
{
    //будет отвечать за все передвижение
    public event Action<float> ChangeFast;
    //ходьба
    private float moveSpeed = 15f;
    private float realSpeed;

    private Rigidbody rb;

    private Vector3 HorizontalMovement;
    private float HorizontalMovementAcceleration = 0.1f;

    //поворот
    private Quaternion LookRotation = Quaternion.identity;
    private float rotationSpeed = 10f;

    //ускорение
    private float fastSpeed = 25f;

    private float currentStamina;
    private float maxValueStamina = 10f;
    private float minValueStamina = 0f;

    private bool FastSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        realSpeed = moveSpeed;
        currentStamina = maxValueStamina;
    }

    private void Update()
    {   //направление со скоростью плавное . Наша медуза и точка умноженная на скорость и  ускорение
        rb.velocity = Vector3.Lerp(rb.velocity, HorizontalMovement.normalized * realSpeed, HorizontalMovementAcceleration);
        //поворот плавный
        transform.rotation = Quaternion.Lerp(transform.rotation, LookRotation, Time.deltaTime * rotationSpeed);

        if (FastSpeed && HorizontalMovement != Vector3.zero)
        {
            currentStamina -= Time.deltaTime * 10f; // но лишь на время

            if (currentStamina <= minValueStamina)
            {
                fastSpeesEnd();
                currentStamina = minValueStamina;
            }
            ChangeFast?.Invoke(currentStamina / maxValueStamina);

        }
        else
        {
            if (currentStamina < maxValueStamina)
            {
                currentStamina += Time.deltaTime / 0.6f; //скорость возращается

                if (currentStamina > maxValueStamina)
                {
                    currentStamina = maxValueStamina;
                }
                ChangeFast?.Invoke(currentStamina / maxValueStamina);

            }
        }
    }

    public void SetVerticalPosition(float offset)
    {
        Vector3 position = transform.position;
        position.y = offset;
        transform.position = position;
    }

    public void ChangeMovementDirection(Vector3 direction) //изменения направления движения 
    {   //мы задаём направление
        HorizontalMovement += direction; //эта точка равна направлению вектор

        if (HorizontalMovement != Vector3.zero)
        {
            ChangeLookingPoint(HorizontalMovement); //передаём в метод точку если она не ноль
        }
    }

    public void ChangeLookingPoint(Vector3 point) //смотрим на точку
    {
        LookRotation = Quaternion.LookRotation(point);
        LookRotation.x = 0; //блочит по этим осям
        LookRotation.z = 0;
    }

    public void fastSpeedStart()
    {
        realSpeed = fastSpeed;
        FastSpeed = true;
    }

    public void fastSpeesEnd()
    {
        realSpeed = moveSpeed;
        FastSpeed = false;
    }
}
