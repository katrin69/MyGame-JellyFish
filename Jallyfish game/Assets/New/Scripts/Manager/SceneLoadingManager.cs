using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    public event Action OnSceneWasLoaded; //событие 

    private Dictionary<EScene, string> Scenes = new Dictionary<EScene, string>(); //словарь со сценами энам 

    private void Awake()
    {
        Scenes.Add(EScene.MainMenu, "MainMenu"); //добавляем сцену из энама в словарь
        Scenes.Add(EScene.Level1, "Level 1");
        Scenes.Add(EScene.GameOver, "GameOver");
    }

    public void LoadScene(EScene scene) //метод куда я передаю сцены
    {
        string sceneName = Scenes[scene]; //имя сцены равно сцена выбранная

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName); //операция в отдельном потоке ? 
        asyncOperation.completed += AsyncOperation_completed; //подписываемся на событие 
    }

    private void AsyncOperation_completed(AsyncOperation asyncOperation) //
    {
        asyncOperation.completed -= AsyncOperation_completed; //отписывается

        if (OnSceneWasLoaded != null)
        {
            OnSceneWasLoaded(); //загружает сцену
        }
    }
}