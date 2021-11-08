using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSceneManager : MonoBehaviour
{
    public Root Root;

    private ResourceManager ResourceManager;
    private SceneLoadingManager SceneLoadingManager;
    private GameOverUIManager GameOverUIManager;

    private void Awake()
    {
        ResourceManager = Root.GetResourceManager();
        SceneLoadingManager = Root.GetSceneManager();

        GameObject canvasObject = ResourceManager.GetObjectInstance(EObjectType.GameOverUI);
        GameOverUIManager = canvasObject.GetComponent<GameOverUIManager>();

        GameOverUIManager.OnRestartButtonClicked += GameOverUIManager_OnRestartButtonClicked;
        GameOverUIManager.OnMainManuButtonClicked += GameOverUIManager_OnMainManuButtonClicked;

        canvasObject.SetActive(true);

    }

    private void GameOverUIManager_OnMainManuButtonClicked()
    {
        SceneLoadingManager.LoadScene(EScene.MainMenu);
    }

    private void GameOverUIManager_OnRestartButtonClicked()
    {
        SceneLoadingManager.LoadScene(EScene.Level1);
    }
}
