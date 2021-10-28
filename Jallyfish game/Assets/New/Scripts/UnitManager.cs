using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    //будет отвечать на связь твоего перса со всеми внутренними системами 

    private PlayerMovement PlayerMovementScript; //скрипт со скоростью 
    private GroundChecker GroundChecker;//скрипт с лучём

    private void Awake()
    {
        PlayerMovementScript = GetComponent<PlayerMovement>();
        GroundChecker = GetComponent<GroundChecker>();
    }

    private void Update()
    {
        float difference = GroundChecker.CheckGround();
        PlayerMovementScript.SetVerticalPosition(difference);
    }

    public void ChangeMovementDirection(Vector3 direction) //метод принимает направление и передаёт в скрипт 
    { 
        PlayerMovementScript.ChangeMovementDirection(direction);
    }

    public void ChangeLookingPoint(Vector3 point) //метод принимает точку и передаёт в скрипт
    {
        PlayerMovementScript.ChangeLookingPoint(point - transform.position);
    }
}
