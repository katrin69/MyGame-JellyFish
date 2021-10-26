using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    //обьект который создаёт все менеджеры. обьединяет

    public GameObject ResourceManagerPrefab;

    private Transform ManagerParent; //для папки

    private SceneLoadingManager SceneLoadingManager; //скрипт со сценами
    private InputManager InputManager;
    private ResourceManager ResourceManager;
    private CameraManager CameraManager;

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

    public SceneLoadingManager GetSceneManager() //метод получения сцен
    {
        if (SceneLoadingManager == null) //если сцен нету то ...
        {
            CreateManager("Scene Manager", out SceneLoadingManager);

            //GameObject gameObject = new GameObject(); //создаём обьект 
            //gameObject.name = "Scene Manager"; //его название
            //gameObject.transform.SetParent(GetManagerParent());  //сеём в папку
            //SceneLoadingManager = gameObject.AddComponent<SceneLoadingManager>(); // записываем туда наш менеджер сцен
        }

        return SceneLoadingManager;
    }

    public InputManager GetInputManager()
    {
        if (InputManager == null)
        {
            CreateManager("Input Manager", out InputManager);
        }

        return InputManager;
    }

    public ResourceManager GetResourceManager()
    {
        if (ResourceManager == null)
        {
            CreateManager("Resource Manager", out ResourceManager, ResourceManagerPrefab);
        }

        return ResourceManager;
    }
    public CameraManager GetCameraManager()
    {
        if (CameraManager == null)
        {
            CreateManager("Camera Manager", out CameraManager);
        }

        return CameraManager;
    }

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
