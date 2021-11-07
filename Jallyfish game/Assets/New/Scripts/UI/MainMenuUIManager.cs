using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    public event Action OnStartButtonClicked;
    public event Action OnExitButtonClicked;

    public Button StartGameButton;
    public Button ExitGameButton;

    private void Awake()
    {
        StartGameButton.onClick.AddListener(StartButtonClicked);
        ExitGameButton.onClick.AddListener(ExitButtonClicked);
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
