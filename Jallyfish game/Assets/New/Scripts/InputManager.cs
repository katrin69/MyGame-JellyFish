using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Ивенты с направлениями нажатий
    public event Action dirNorth;
    public event Action dirSouth;
    public event Action dirWest;
    public event Action dirEast;

    //стрельба
    public event Action shoot;

    //движение мыши
    public event Action<Vector3> positionMouse;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W)) //если нажато W и dirNorth не пустое то dirNorth ивент
        {
            if (dirNorth != null)
            {
                dirNorth();
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (dirSouth != null)
            {
                dirSouth();
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (dirWest != null)
            {
                dirWest();
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (dirEast != null)
            {
                dirEast();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
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
