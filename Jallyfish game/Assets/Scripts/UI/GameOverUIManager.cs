using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{
    public event Action OnRestartButtonClicked; //события для клика по кнопке
    public event Action OnMainManuButtonClicked;

    public Button ReplayButton; //сами кнопки
    public Button MainManuButton;

    private void Start()
    {
        ReplayButton.onClick.AddListener(RestartButtonClicked); //у кнопки вызывает метод клик куда передаётся событие клика
        MainManuButton.onClick.AddListener(MainManuButtonClicked);

    }

    public void RestartButtonClicked() //метод 
    {
        if (OnRestartButtonClicked != null)
        {
            OnRestartButtonClicked();
        }
    }

    public void MainManuButtonClicked()
    {
        if (OnMainManuButtonClicked != null)
        {
            OnMainManuButtonClicked();
        }
    }
}
