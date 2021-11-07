using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Сккрипт отвечает на движение . Стрельбу

    //Ивенты с направлениями нажатий
    public event Action dirNorthStart;
    public event Action dirNorthEnd;

    public event Action dirSouthStart;
    public event Action dirSouthEnd;

    public event Action dirWestStart;
    public event Action dirWestEnd;

    public event Action dirEastStart;
    public event Action dirEastEnd;

    //стрельба
    public event Action shoot;

    //ускорение
    public event Action fastSpeedStart;
    public event Action fastSpeedEnd;

    //движение мыши
    public event Action<Vector3> positionMouse;
    
    //выбор оружия
    public event Action choosWeaponOne;
    public event Action choosWeaponTwo;
    public event Action choosWeaponThree;
    public event Action choosWeaponFour;

    //пауза
    public event Action chooseEcsButton;

    private void Update()
    {
        //выбор оружия
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (choosWeaponOne != null)
            {
                choosWeaponOne();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (choosWeaponTwo != null)
            {
                choosWeaponTwo();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (choosWeaponThree != null)
            {
                choosWeaponThree();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (choosWeaponFour != null)
            {
                choosWeaponFour();
            }
        }


        //пауза
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (chooseEcsButton != null)
            {
                chooseEcsButton();
            }
        }


        //ускорение
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            if (fastSpeedStart != null)
            {
                fastSpeedStart();
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (fastSpeedEnd != null)
            {
                fastSpeedEnd();
            }
        }

        //ходьба
        if (Input.GetKeyDown(KeyCode.W)) //если нажато W и dirNorth не пустое то dirNorth ивент
        {
            if (dirNorthStart != null)
            {
                dirNorthStart();
            }
        }
        if (Input.GetKeyUp(KeyCode.W)) //если нажато W и dirNorth не пустое то dirNorth ивент
        {
            if (dirNorthEnd != null)
            {
                dirNorthEnd();
            }
        }



        if (Input.GetKeyDown(KeyCode.S))
        {
            if (dirSouthStart != null)
            {
                dirSouthStart();
            }
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            if (dirSouthEnd != null)
            {
                dirSouthEnd();
            }
        }



        if (Input.GetKeyDown(KeyCode.A))
        {
            if (dirWestStart != null)
            {
                dirWestStart();
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            if (dirWestEnd != null)
            {
                dirWestEnd();
            }
        }



        if (Input.GetKeyDown(KeyCode.D))
        {
            if (dirEastStart != null)
            {
                dirEastStart();
            }
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            if (dirEastEnd != null)
            {
                dirEastEnd();
            }
        }



        if (Input.GetKeyDown(KeyCode.Mouse0)) //Стреляние
        {
            if (shoot != null)
            {
                shoot();
            }
        }

        if (positionMouse != null) //проверяет подписку
        {
            positionMouse(Input.mousePosition);
        }
    }
}
