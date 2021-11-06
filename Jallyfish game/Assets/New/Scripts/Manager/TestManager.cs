using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    //обьект который отвечает за то чтобы на текущей сцене было все ок и проверяет как все работает

    public Root Root; //берём Root
    public Camera Camera;

    public Transform SpawnObject; //место появления

    public Transform LeftDown;
    public Transform LeftUp;
    public Transform RightDown;

    private SceneLoadingManager SceneLoadingManager; //скрипт с загрузкой сцен
    private InputManager InputManager; //скрипт с управлением
    private ResourceManager ResourceManager; //скрипт с ресурсами(акула медуза пуля)
    private CameraManager CameraManager; //скрипт с камерой

    private UnitManager UnitManager; //скрипт через который проходит управление персонажем

    private EnemyInstantiationManager EnemyInstantiationManager;
    private CanvasManager CanvasManager; //наш канвас

    //управление направления
    private Vector3 WestDirection = Vector3.left;
    private Vector3 NorthDirection = Vector3.forward;

    private Vector3 CurrentMousePosition;

    private void Awake()
    {
        SceneLoadingManager = Root.GetSceneManager(); //присваеваем метод из Root который получает сцены 
        InputManager = Root.GetInputManager(); //присваем метод который получает управление персом

        //выбор оружия
        InputManager.choosWeaponOne += InputManager_choosWeaponOne; //подписываемся на событие
        InputManager.choosWeaponTwo += InputManager_choosWeaponTwo;
        InputManager.choosWeaponThree += InputManager_choosWeaponThree;
        InputManager.choosWeaponFour += InputManager_choosWeaponFour;

        //ускорение
        InputManager.fastSpeedStart += InputManager_fastSpeedStart;
        InputManager.fastSpeedEnd += InputManager_fastSpeedEnd;

        //движение перса
        InputManager.dirNorthStart += InputManager_dirNorthStart;
        InputManager.dirNorthEnd += InputManager_dirNorthEnd;

        InputManager.dirSouthStart += InputManager_dirSouthStart;
        InputManager.dirSouthEnd += InputManager_dirSouthEnd;

        InputManager.dirWestStart += InputManager_dirWestStart;
        InputManager.dirEastEnd += InputManager_dirEastEnd;

        InputManager.dirEastStart += InputManager_dirEastStart;
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
        UnitManager.Init(ResourceManager);
        UnitManager.WeaponColldownChanged += UnitManager_WeaponColldownChanged;
        UnitManager.ChangeHealth += UnitManager_ChangeHealth;
        UnitManager.ChangeArmor += UnitManager_ChangeArmor;
        UnitManager.ChangeFast += UnitManager_ChangeFast;

        //присваеваем метод из Root который получает скрипт камеру и передаём камеру с игроком
        CameraManager = Root.GetCameraManager();
        CameraManager.Initialize(Camera, jellyFish.transform);

        EnemyInstantiationManager = Root.GetEnemyInstantiationManager();

        Vector4 spawningZone = new Vector4(LeftDown.position.z, LeftUp.position.z, LeftDown.position.x, RightDown.position.x);

        EnemyInstantiationManager.Init(ResourceManager, jellyFish.transform, spawningZone);

        //наш канвас
        CanvasManager = Root.GetCanvasManager();
        CanvasManager.choosWeaponOne += InputManager_choosWeaponOne;
        CanvasManager.choosWeaponTwo += InputManager_choosWeaponTwo;
        CanvasManager.choosWeaponThree += InputManager_choosWeaponThree;
        CanvasManager.choosWeaponFour += InputManager_choosWeaponFour;
    }

    private void UnitManager_ChangeFast(float curStam)
    {
        CanvasManager.ChangeStamina(curStam);
    }

    private void UnitManager_ChangeArmor(float curArmor)
    {
        CanvasManager.ChangeArmor(curArmor);
    }

    private void UnitManager_ChangeHealth(float curHp)
    {
        CanvasManager.ChangeHealthe(curHp);
    }

    private void UnitManager_WeaponColldownChanged(EWeapon weapon, float cooldownPercent)
    {
        CanvasManager.SetWeaponFiller(weapon, cooldownPercent);
    }

    //выбор оружия
    private void InputManager_choosWeaponOne() //обработчик событий
    {
        UnitManager.ChoosWeaponOne();
    }

    private void InputManager_choosWeaponTwo()
    {
        UnitManager.ChoosWeaponTwo();
    }

    private void InputManager_choosWeaponThree()
    {
        UnitManager.ChoosWeaponThree();
    }

    private void InputManager_choosWeaponFour()
    {
        UnitManager.ChoosWeaponFour();
    } 

    //позиция мыши
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

        UnitManager.Shoot();
    }

    //ускорение
    private void InputManager_fastSpeedStart()
    {
        UnitManager.fastSpeedStart();
    }

    private void InputManager_fastSpeedEnd()
    {
        UnitManager.fastSpeesEnd();
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
