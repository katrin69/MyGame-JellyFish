using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
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

    //движение мыши
    public event Action<Vector3> positionMouse;

    private void Update()
    {
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
