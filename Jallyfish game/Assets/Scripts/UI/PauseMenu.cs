using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false; //меню не включено
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
        Time.timeScale = 1f; 
        GameIsPause = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);//меню включено
        Time.timeScale = 0f; 
        GameIsPause = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f; //переход в клавное меню
        SceneManager.LoadScene("MainMenu");
    }
}
