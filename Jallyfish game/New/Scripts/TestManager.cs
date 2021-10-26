using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    //обьект который отвечает за то чтобы на текущей сцене было все ок \проверяет как все работает

    public Root Root; //берём Root
    public Camera Camera;

    public Transform SpawnObject;

    private SceneLoadingManager SceneLoadingManager; //скрипт с загрузкой сцен
    private InputManager InputManager;
    private ResourceManager ResourceManager;
    private CameraManager CameraManager;

    private UnitManager UnitManager;

    private Vector3 WestDirection = Vector3.left;
    private Vector3 NorthDirection = Vector3.forward;

    private Vector3 CurrentMousePosition;

    private void Awake()
    {
        SceneLoadingManager = Root.GetSceneManager(); //присваеваем метод из Root который получает сцены 
        InputManager = Root.GetInputManager();

        InputManager.dirSouthStart += InputManager_dirSouthStart; //
        InputManager.dirNorthStart += InputManager_dirNorthStart;
        InputManager.dirEastStart += InputManager_dirEastStart;
        InputManager.dirWestStart += InputManager_dirWestStart;

        InputManager.dirSouthEnd += InputManager_dirSouthEnd;
        InputManager.dirNorthEnd += InputManager_dirNorthEnd;
        InputManager.dirEastEnd += InputManager_dirEastEnd;
        InputManager.dirWestEnd += InputManager_dirWestEnd;

        InputManager.shoot += InputManager_shoot;
        InputManager.positionMouse += InputManager_positionMouse;

        ResourceManager = Root.GetResourceManager();

        GameObject jellyFish = ResourceManager.GetObjectInstance(EObjectType.Jellyfish);
        jellyFish.transform.position = SpawnObject.position;
        jellyFish.SetActive(true);
        UnitManager = jellyFish.GetComponent<UnitManager>();

        CameraManager = Root.GetCameraManager();
        CameraManager.Initialize(Camera, jellyFish.transform);
    }

    private void InputManager_positionMouse(Vector3 mousePosition)
    {
        CurrentMousePosition = mousePosition;
    }

    private void InputManager_shoot()
    {
        if (CameraManager.GetGroundPoint(CurrentMousePosition, out Vector3 groundPoint))
        {
            UnitManager.ChangeLookingPoint(groundPoint);
        }
    }

    private void InputManager_dirWestStart()
    {
        //Debug.Log("DIR WEST");
        UnitManager.ChangeMovementDirection(WestDirection);
    }

    private void InputManager_dirWestEnd()
    {
        UnitManager.ChangeMovementDirection(-WestDirection);
    }

    private void InputManager_dirEastStart()
    {
        //Debug.Log("DIR EAST");
        UnitManager.ChangeMovementDirection(-WestDirection);
    }

    private void InputManager_dirEastEnd()
    {
        UnitManager.ChangeMovementDirection(WestDirection);
    }

    private void InputManager_dirNorthStart()
    {
        //Debug.Log("DIR NORTH");
        UnitManager.ChangeMovementDirection(NorthDirection);
    }

    private void InputManager_dirNorthEnd()
    {
        UnitManager.ChangeMovementDirection(-NorthDirection);
    }

    private void InputManager_dirSouthStart()
    {
        //Debug.Log("DIR SOUTH");
        UnitManager.ChangeMovementDirection(-NorthDirection);
    }

    private void InputManager_dirSouthEnd()
    {
        UnitManager.ChangeMovementDirection(NorthDirection);
    }
}
