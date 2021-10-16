using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    //обьект который отвечает за то чтобы на текущей сцене было все ок \проверяет как все работает

    public Root Root; //берём Root

    public Transform SpawnObject;

    private SceneLoadingManager SceneLoadingManager; //скрипт с загрузкой сцен
    private InputManager InputManager;
    private ResourceManager ResourceManager;

    private void Awake()
    {
        SceneLoadingManager = Root.GetSceneManager(); //присваеваем метод из Root который получает сцены 
        InputManager = Root.GetInputManager();

        InputManager.dirSouth += InputManager_dirSouth; //
        InputManager.dirNorth += InputManager_dirNorth;
        InputManager.dirEast += InputManager_dirEast;
        InputManager.dirWest += InputManager_dirWest; ;
        InputManager.shoot += InputManager_shoot;
        InputManager.positionMouse += InputManager_positionMouse;

        ResourceManager = Root.GetResourceManager();
        GameObject jellyFish = ResourceManager.GetObjectInstance(EObjectType.Jellyfish);
        jellyFish.transform.position = SpawnObject.position;
        jellyFish.SetActive(true);
    }

    private void InputManager_positionMouse(Vector3 mousePosition)
    {

    }

    private void InputManager_shoot()
    {
        //Debug.Log("SHOOT");
    }

    private void InputManager_dirWest()
    {
        //Debug.Log("DIR WEST");
    }

    private void InputManager_dirEast()
    {
        //Debug.Log("DIR EAST");
    }

    private void InputManager_dirNorth()
    {
        //Debug.Log("DIR NORTH");
    }

    private void InputManager_dirSouth()
    {
        //Debug.Log("DIR SOUTH");
    }
}
