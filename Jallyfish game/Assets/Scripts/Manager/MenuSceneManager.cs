using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneManager : MonoBehaviour
{
    public Root Root; //берём Root

    private ResourceManager ResourceManager;
    private SceneLoadingManager SceneLoadingManager;
    private MainMenuUIManager MainMenuUIManager;
    private AudioManager AudioManager;
    private SaverManager SaverManager;

    private void Awake()
    {
        ResourceManager = Root.GetResourceManager();

        SceneLoadingManager = Root.GetSceneManager();

        SaverManager = Root.GetSaverManager();

        GameObject canvasObject = ResourceManager.GetObjectInstance(EObjectType.MainMenuUI);
        MainMenuUIManager = canvasObject.GetComponent<MainMenuUIManager>();

        MainMenuUIManager.OnStartButtonClicked += MainMenuUIManager_OnStartButtonClicked;
        MainMenuUIManager.OnExitButtonClicked += MainMenuUIManager_OnExitButtonClicked;

        canvasObject.SetActive(true);

        AudioManager = Root.GetAudioManager(); //берём музло из рута
        AudioManager.Play("MusicMainMenu"); //добавялем музло в меню на фон
    }

    private void Start()
    {
        bool isContinueButtonActive = SaverManager.IsSaveDataExists(); //имеет ли игра сохранение
        MainMenuUIManager.SetContinueButtonStatus(isContinueButtonActive); //кнопка континуе активная или нет
    }

    private void MainMenuUIManager_OnStartButtonClicked()
    {
        SceneLoadingManager.LoadScene(EScene.Level1);
    }

    private void MainMenuUIManager_OnExitButtonClicked()
    {
        if (Application.isEditor)
        {
            Debug.Log("Exit!"); //выход битч
        }
        else
        {
            Application.Quit();            
        }
    }
}
