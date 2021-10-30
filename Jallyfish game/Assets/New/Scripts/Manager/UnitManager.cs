using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    //будет отвечать на связь твоего перса со всеми внутренними системами 

    private PlayerMovement PlayerMovementScript; //скрипт со скоростью 
    private GroundChecker GroundChecker;//скрипт с лучём
    private WeaponManager WeaponManager; //скрипит с видами оружия

    private void Awake()
    {
        PlayerMovementScript = GetComponent<PlayerMovement>();
        GroundChecker = GetComponent<GroundChecker>();
        WeaponManager = GetComponent<WeaponManager>();
    }

    private void Update()
    {
        float difference = GroundChecker.CheckGround();
        PlayerMovementScript.SetVerticalPosition(difference);
    }

    //методы для выбора оружия


    //метод принимает направление и передаёт в скрипт 
    public void ChangeMovementDirection(Vector3 direction) 
    {
        PlayerMovementScript.ChangeMovementDirection(direction);
    }

    //метод принимает точку и передаёт в скрипт
    public void ChangeLookingPoint(Vector3 point) 
    {
        PlayerMovementScript.ChangeLookingPoint(point - transform.position);
    }


    //метод ускорение
    public void fastSpeedStart() //вызываем метод
    {
        PlayerMovementScript.fastSpeedStart();
    }

    public void fastSpeesEnd()
    {
        PlayerMovementScript.fastSpeesEnd();

    }
}
