using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    //обьект который создаёт все менеджеры. обьединяет

    public GameObject ResourceManagerPrefab;

    private Transform ManagerParent; //для папки

    private SceneLoadingManager SceneLoadingManager; //скрипт со сценами
    private InputManager InputManager; //скрипт с движением игрока
    private ResourceManager ResourceManager; //скрипт с ресурсами(акула медуза пуля)
    private CameraManager CameraManager; //скрипт с камерой
    private WeaponManager WeaponManager; //скрипт с оружием
    private EnemyInstantiationManager EnemyInstantiationManager;
    
    private Transform GetManagerParent()
    {
        if (ManagerParent == null)
        {
            //создаём папку и суём туда все менеджеры
            GameObject parent = new GameObject();
            parent.name = "Managers";
            ManagerParent = parent.transform;
        }

        return ManagerParent;
    }  

    public WeaponManager GetWeaponManager()
    {
        if (WeaponManager == null) //если сцен нету то ...
        {
            CreateManager("Weapon Manager", out WeaponManager);
        }

        return WeaponManager;
    }

    public SceneLoadingManager GetSceneManager() //метод получения сцен
    {
        if (SceneLoadingManager == null) //если сцен нету то ...
        {
            CreateManager("Scene Manager", out SceneLoadingManager);
        }

        return SceneLoadingManager;
    }

    public InputManager GetInputManager() //метод получения управление персом
    {
        if (InputManager == null)
        {
            CreateManager("Input Manager", out InputManager);
        }

        return InputManager;
    }

    public ResourceManager GetResourceManager() //метод получения ресурсами
    {
        if (ResourceManager == null)
        {
            CreateManager("Resource Manager", out ResourceManager, ResourceManagerPrefab);
        }

        return ResourceManager;
    }
    public CameraManager GetCameraManager() //метод получения камеры
    {
        if (CameraManager == null)
        {
            CreateManager("Camera Manager", out CameraManager);
        }

        return CameraManager;
    }

    public EnemyInstantiationManager GetEnemyInstantiationManager() //метод получения врага
    {
        if (EnemyInstantiationManager == null)
        {
            CreateManager("Enemy Instantiation Manager", out EnemyInstantiationManager);
        }

        return EnemyInstantiationManager;
    }

    //дженерик
    private void CreateManager<ManagerType>(string name, out ManagerType manager, GameObject prefab = null) where ManagerType : MonoBehaviour
    {
        GameObject gameObject;

        if (prefab == null)
        {
            gameObject = new GameObject();
            manager = gameObject.AddComponent<ManagerType>();
        }
        else
        {
            gameObject = Instantiate(prefab);
            manager = gameObject.GetComponent<ManagerType>();
        }

        gameObject.name = name;

        Transform parent = GetManagerParent();
        gameObject.transform.SetParent(parent);
    }
}
