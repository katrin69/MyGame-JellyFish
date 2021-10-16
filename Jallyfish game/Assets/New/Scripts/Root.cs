using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    //обьект который создаёт все менеджеры. обьединяет

    public GameObject ResourceManagerPrefab;

    private SceneLoadingManager SceneLoadingManager; //скрипт со сценами
    private InputManager InputManager;
    private ResourceManager ResourceManager;

    public SceneLoadingManager GetSceneManager() //метод получения сцен
    {
        if (SceneLoadingManager == null) //если сцен нету то ...
        {
            GameObject gameObject = new GameObject(); //создаём обьект 
            gameObject.name = "Scene Manager"; //его название
            SceneLoadingManager = gameObject.AddComponent<SceneLoadingManager>(); // записываем туда наш менеджер сцен
        }

        return SceneLoadingManager;
    }

    public InputManager GetInputManager()
    {
        if (InputManager == null)
        {
            GameObject gameObject = new GameObject();
            gameObject.name = "Input Manager";
            InputManager = gameObject.AddComponent<InputManager>();
        }

        return InputManager;
    }
    
    public ResourceManager GetResourceManager()
    {
        if (ResourceManager == null)
        {
            GameObject resourceManagerGameObject = Instantiate(ResourceManagerPrefab);
            resourceManagerGameObject.name = "Resource Manager";

            ResourceManager = resourceManagerGameObject.GetComponent<ResourceManager>();
        }

        return ResourceManager;
    }
}
