using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    //обьект который отвечает за то чтобы на текущей сцене было все ок и проверяет как все работает

    public bool SpawnEnemies  = false;
    private bool IsPaused = false;

    [Space(10)]
    public Root Root; //берём Root
    public Camera Camera;

    public Transform SpawnObject; //место появления

    public List<EnemySpawner> Spawners;

    private SceneLoadingManager SceneLoadingManager; //скрипт с загрузкой сцен
    private InputManager InputManager; //скрипт с управлением
    private ResourceManager ResourceManager; //скрипт с ресурсами(акула медуза пуля)
    private CameraManager CameraManager; //скрипт с камерой

    private UnitManager UnitManager; //скрипт через который проходит управление персонажем

    private EnemyInstantiationManager EnemyInstantiationManager;
    private GameUIManager GameUIManager; //наш канвас
    private AudioManager AudioManager;
    private SaverManager SaverManager;

    //управление направления
    private Vector3 WestDirection = Vector3.left;
    private Vector3 NorthDirection = Vector3.forward;

    private Vector3 CurrentMousePosition;

    private void Awake()
    {
        SceneLoadingManager = Root.GetSceneManager(); //присваеваем метод из Root который получает сцены 

        AudioManager = Root.GetAudioManager();

        SaverManager = Root.GetSaverManager();

        InputManager = Root.GetInputManager(); //присваем метод который получает управление п

        ResourceManager = Root.GetResourceManager();//присваеваем метод из root который получает скрипт с ресурсами(вкула медуза пуля)

        //сохранение и загрузка
        InputManager.saveGame += OnSaveGame;
        InputManager.loadGame += OnLoadGame;

        EventPlayerOn();

        //создаём обьект медуза . ResourceManager вызывает метод через который мы получаем обьект
        GameObject jellyFish = ResourceManager.GetObjectInstance(EObjectType.Jellyfish);
        //передаём ей место появления
        jellyFish.transform.position = SpawnObject.position;
        jellyFish.SetActive(true);

        //добавляем в медузу управление
        UnitManager = jellyFish.GetComponent<UnitManager>();
        UnitManager.Init(ResourceManager, AudioManager);
        UnitManager.WeaponColldownChanged += UnitManager_WeaponColldownChanged;
        UnitManager.ChangeHealth += UnitManager_ChangeHealth;
        UnitManager.ChangeArmor += UnitManager_ChangeArmor;
        UnitManager.ChangeFast += UnitManager_ChangeFast;
        UnitManager.ChangeLevel += UnitManager_ChangeLevel;

        //присваеваем метод из Root который получает скрипт камеру и передаём камеру с игроком
        CameraManager = Root.GetCameraManager();
        CameraManager.Initialize(Camera, jellyFish.transform);

        //наш канвас
        GameObject canvasObject = ResourceManager.GetObjectInstance(EObjectType.GameUI);
        GameUIManager = canvasObject.GetComponent<GameUIManager>();
        GameUIManager.choosWeaponOne += InputManager_choosWeaponOne;
        GameUIManager.choosWeaponTwo += InputManager_choosWeaponTwo;
        GameUIManager.choosWeaponThree += InputManager_choosWeaponThree;
        GameUIManager.choosWeaponFour += InputManager_choosWeaponFour;
        GameUIManager.OnBackMainMenu += GameUIManager_OnBackMainMenu;
        GameUIManager.OnContinue += GameUIManager_OnContinue;
        canvasObject.SetActive(true);

        //сохранение
        GameUIManager.OnSaveGame += OnSaveGame;

        //пауза
        InputManager.chooseEcsButton += InputManager_bottonEsc;

        //проигрыш
        UnitManager.PlayerDead += UnitManager_PlayerDead;    
    }

 

    private void Start() //действия при старте игры
    {
        if (SpawnEnemies) //если акулы значит у нас тут появляеются то 
        {
            EnemyInstantiationManager = Root.GetEnemyInstantiationManager(); //берём из рута менеджер инициализации акул
            EnemyInstantiationManager.Init(ResourceManager, AudioManager, CameraManager); //добавляем им музло там
            EnemyInstantiationManager.InstantiateEnemies(Spawners); //инициализируем акула
        }
        //начинает играть музыка на фоне 
        AudioManager.Play("MusicBackground");
        AudioManager.Play("MusicInGame");
    }

    //метод с подпискамина события управления персонажем ,ходьба тау тау тау
    private void EventPlayerOn()
    {
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
    }

    private void EventPlayerOff()
    {
        //выбор оружия
        InputManager.choosWeaponOne -= InputManager_choosWeaponOne; //подписываемся на событие
        InputManager.choosWeaponTwo -= InputManager_choosWeaponTwo;
        InputManager.choosWeaponThree -= InputManager_choosWeaponThree;
        InputManager.choosWeaponFour -= InputManager_choosWeaponFour;

        //ускорение
        InputManager.fastSpeedStart -= InputManager_fastSpeedStart;
        InputManager.fastSpeedEnd -= InputManager_fastSpeedEnd;

        //движение перса
        InputManager.dirNorthStart -= InputManager_dirNorthStart;
        InputManager.dirNorthEnd -= InputManager_dirNorthEnd;

        InputManager.dirSouthStart -= InputManager_dirSouthStart;
        InputManager.dirSouthEnd -= InputManager_dirSouthEnd;

        InputManager.dirWestStart -= InputManager_dirWestStart;
        InputManager.dirEastEnd -= InputManager_dirEastEnd;

        InputManager.dirEastStart -= InputManager_dirEastStart;
        InputManager.dirWestEnd -= InputManager_dirWestEnd;

        //стрельба
        InputManager.shoot -= InputManager_shoot;
        InputManager.positionMouse -= InputManager_positionMouse;
    }

    //пауза
    public void PauseCheck() //включает паузу
    {
        if (IsPaused) //если выключает паузу то IsPaused = false
        {
            Unpause(); //и пауза выклюяается
        }
        else
        {
            Pause(); //включчается пауза
        }

        GameUIManager.SetPauseMenuActive(IsPaused); //или включена или нет
    }

    private void Unpause()// метод чтобы отключить паузу
    {
        EventPlayerOn();
        Time.timeScale = 1f;
        IsPaused = false;
    }

    private void Pause() //вклюает паузу и время останавливается
    {
        EventPlayerOff();
        Time.timeScale = 0f;
        IsPaused = true;
    }

    private void InputManager_bottonEsc()//обработчик событий
    {
        PauseCheck();
    }

    private void GameUIManager_OnContinue()
    {
        PauseCheck();
    }

    //загрузка сцены главное меню
    private void GameUIManager_OnBackMainMenu()//обработчик событий
    {
        Unpause();
        SceneLoadingManager.LoadScene(EScene.MainMenu);
    }

    //игрок умер загружаем сцену проигрыша
    private void UnitManager_PlayerDead()
    {
        SceneLoadingManager.LoadScene(EScene.GameOver);
    }


    

    //загрузка
    private void OnLoadGame()
    {
        if (SaverManager.IsSaveDataExists())
        {
            SaverData saverData = SaverManager.Load();

            UnitManager.ApplySaverData(saverData);
            EnemyInstantiationManager.ApplySaverData(saverData);
        }
    }
    //сохранение
    private void OnSaveGame()
    {
        SaverData saverData = new SaverData();

        UnitManager.FillSaverData(saverData);

        if (EnemyInstantiationManager != null)
        {
            EnemyInstantiationManager.FillSaverData(saverData);
        }

        SaverManager.Save(saverData);
    }

  


    //изменение скорости
    private void UnitManager_ChangeFast(float curStam)
    {
        GameUIManager.ChangeStamina(curStam);
    }
    //изменение щита
    private void UnitManager_ChangeArmor(float curArmor)
    {
        GameUIManager.ChangeArmor(curArmor);
    }
    //изменение жизни
    private void UnitManager_ChangeHealth(float curHp)
    {
        GameUIManager.ChangeHealthe(curHp);
    }
    //изменение уровня
    private void UnitManager_ChangeLevel(float level) //Лэвл
    {
        GameUIManager.ChangeLevel(level);
    }
    private void UnitManager_WeaponColldownChanged(EWeapon weapon, float cooldownPercent)
    {
        GameUIManager.SetWeaponFiller(weapon, cooldownPercent);
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
        if (CameraManager.GetClickPoint(CurrentMousePosition, out Vector3 groundPoint))
        {
            UnitManager.ChangeLookingPoint(groundPoint);
        }

        UnitManager.Shoot();
    }

    //ускорение
    private void InputManager_fastSpeedStart()  //обработчик событий
    {
        AudioManager.Play("SoundFastSpeed");
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
