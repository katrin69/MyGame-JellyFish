using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false; //при старте паузы нет
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; //скорость игры восстанавливается
        GameIsPause = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);//Позволит вызывать меню паузы.Меню появляется во время паузы
        Time.timeScale = 0f; //все замрёт кроме мышки
        GameIsPause = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f; //должен убирать баг 
        SceneManager.LoadScene("MainMenu");
    }
}
