using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    //будет отвечать на связь твоего перса со всеми внутренними системами 

    private PlayerMovement PlayerMovementScript;
    private GroundChecker GroundChecker;

    private void Awake()
    {
        PlayerMovementScript = GetComponent<PlayerMovement>();
        GroundChecker = GetComponent<GroundChecker>();
    }

    private void Update()
    {
        float difference = GroundChecker.CheckGround();
        PlayerMovementScript.SetVerticalPosition(difference);
        //PlayerMovementScript.ChangeMovementDirection(Vector3.up * difference);
    }

    public void ChangeMovementDirection(Vector3 direction)
    { 
        PlayerMovementScript.ChangeMovementDirection(direction);
    }

    public void ChangeLookingPoint(Vector3 point)
    {
        PlayerMovementScript.ChangeLookingPoint(point - transform.position);
    }
}
