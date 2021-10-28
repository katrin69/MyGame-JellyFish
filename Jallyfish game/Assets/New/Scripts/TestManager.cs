using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    //обьект который отвечает за то чтобы на текущей сцене было все ок и проверяет как все работает

    public Root Root; //берём Root
    public Camera Camera;

    public Transform SpawnObject; //место появления

    private SceneLoadingManager SceneLoadingManager; //скрипт с загрузкой сцен
    private InputManager InputManager; //скрипт с управлением
    private ResourceManager ResourceManager; //скрипт с ресурсами(акула медуза пуля)
    private CameraManager CameraManager; //скрипт с камерой

    private UnitManager UnitManager; //скрипт через который проходит управление персонажем

    //управление направления
    private Vector3 WestDirection = Vector3.left;
    private Vector3 NorthDirection = Vector3.forward;

    private Vector3 CurrentMousePosition;

    private void Awake()
    {
        SceneLoadingManager = Root.GetSceneManager(); //присваеваем метод из Root который получает сцены 
        InputManager = Root.GetInputManager(); //присваем метод который получает управление персом

        //движение перса
        InputManager.dirSouthStart += InputManager_dirSouthStart; 
        InputManager.dirNorthStart += InputManager_dirNorthStart;

        InputManager.dirEastStart += InputManager_dirEastStart;
        InputManager.dirWestStart += InputManager_dirWestStart;

        InputManager.dirSouthEnd += InputManager_dirSouthEnd;
        InputManager.dirNorthEnd += InputManager_dirNorthEnd;

        InputManager.dirEastEnd += InputManager_dirEastEnd;
        InputManager.dirWestEnd += InputManager_dirWestEnd;


        //стрельба
        InputManager.shoot += InputManager_shoot;
        InputManager.positionMouse += InputManager_positionMouse;

        //присваеваем метод из root который получает скрипт с ресурсами(вкула медуза пуля)
        ResourceManager = Root.GetResourceManager();

        //создаём обьект медуза . ResourceManager вызывает метод через который мы получаем обьект
        GameObject jellyFish = ResourceManager.GetObjectInstance(EObjectType.Jellyfish);
        //передаём ей место появления
        jellyFish.transform.position = SpawnObject.position;
        jellyFish.SetActive(true);
        //добавляем в медузу управление
        UnitManager = jellyFish.GetComponent<UnitManager>();

        //присваеваем метод из Root который получает скрипт камеру и передаём камеру с игроком
        CameraManager = Root.GetCameraManager();
        CameraManager.Initialize(Camera, jellyFish.transform);
    }

    private void InputManager_positionMouse(Vector3 mousePosition)
    {
        CurrentMousePosition = mousePosition;
    }

    //стрельба
    private void InputManager_shoot()
    {
        if (CameraManager.GetGroundPoint(CurrentMousePosition, out Vector3 groundPoint))
        {
            UnitManager.ChangeLookingPoint(groundPoint);
        }
    }


    //ходьба
    private void InputManager_dirWestStart()
    {
        UnitManager.ChangeMovementDirection(WestDirection);
    }

    private void InputManager_dirWestEnd()
    {
        UnitManager.ChangeMovementDirection(-WestDirection);
    }


    private void InputManager_dirEastStart()
    {
        UnitManager.ChangeMovementDirection(-WestDirection);
    }

    private void InputManager_dirEastEnd()
    {
        UnitManager.ChangeMovementDirection(WestDirection);
    }


    private void InputManager_dirNorthStart()
    {
        UnitManager.ChangeMovementDirection(NorthDirection);
    }

    private void InputManager_dirNorthEnd()
    {
        UnitManager.ChangeMovementDirection(-NorthDirection);
    }


    private void InputManager_dirSouthStart()
    {
        UnitManager.ChangeMovementDirection(-NorthDirection);
    }

    private void InputManager_dirSouthEnd()
    {
        UnitManager.ChangeMovementDirection(NorthDirection);
    }
}
