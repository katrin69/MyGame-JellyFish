using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    public event Action OnExitContinueClicked;
    public event Action OnStartButtonClicked; //события для клика по кнопке
    public event Action OnExitButtonClicked;

    public Button ContinueButton;
    public Button StartGameButton; //сами кнопки
    public Button ExitGameButton;

    private void Awake()
    {
        ContinueButton.onClick.AddListener(ContinueClicked);
        StartGameButton.onClick.AddListener(StartButtonClicked);//у кнопки вызывает метод клик куда передаётся событие клика
        ExitGameButton.onClick.AddListener(ExitButtonClicked);
    }

    private void ContinueClicked()
    {
        if (OnExitContinueClicked != null)
        {
            OnExitContinueClicked();
        }
    }
    private void StartButtonClicked()
    {
        if (OnStartButtonClicked != null)
        {
            OnStartButtonClicked();
        }
    }

    private void ExitButtonClicked()
    {
        if (OnExitButtonClicked != null)
        {
            OnExitButtonClicked();
        }
    }
}
