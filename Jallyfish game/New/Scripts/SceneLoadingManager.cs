using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    public event Action OnSceneWasLoaded;

    private Dictionary<EScene, string> Scenes = new Dictionary<EScene, string>();

    private void Awake()
    {
        Scenes.Add(EScene.MainMenu, "NewScene");
        Scenes.Add(EScene.Pause, "NewScene");
        Scenes.Add(EScene.Level1, "NewScene");
    }

    public void LoadScene(EScene scene) //метод куда я передаю сцены
    {
        string sceneName = Scenes[scene];

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.completed += AsyncOperation_completed;
    }

    private void AsyncOperation_completed(AsyncOperation asyncOperation)
    {
        asyncOperation.completed -= AsyncOperation_completed;

        if (OnSceneWasLoaded != null)
        {
            OnSceneWasLoaded();
        }
    }
}