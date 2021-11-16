using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSceneManager : MonoBehaviour
{
    public Root Root;

    private ResourceManager ResourceManager;
    private SceneLoadingManager SceneLoadingManager;
    private GameOverUIManager GameOverUIManager;
    private AudioManager AudioManager;

    private void Awake()
    {
        ResourceManager = Root.GetResourceManager();
        SceneLoadingManager = Root.GetSceneManager();

        GameObject canvasObject = ResourceManager.GetObjectInstance(EObjectType.GameOverUI);
        GameOverUIManager = canvasObject.GetComponent<GameOverUIManager>();

        GameOverUIManager.OnRestartButtonClicked += GameOverUIManager_OnRestartButtonClicked;
        GameOverUIManager.OnMainManuButtonClicked += GameOverUIManager_OnMainManuButtonClicked;

        canvasObject.SetActive(true);

        AudioManager = Root.GetAudioManager();
        AudioManager.Play("MusicInGame");
    }

    private void GameOverUIManager_OnMainManuButtonClicked()
    {
        AudioManager.Play("ClickOnBotton");
        SceneLoadingManager.LoadScene(EScene.MainMenu);
    }

    private void GameOverUIManager_OnRestartButtonClicked()
    {
        AudioManager.Play("ClickOnBotton");
        SceneLoadingManager.LoadScene(EScene.Level1);
    }
}
