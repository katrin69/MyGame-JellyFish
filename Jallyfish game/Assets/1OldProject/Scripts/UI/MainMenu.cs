 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //метод запускается из кнопки поэтому public
    public void OpenScene(int index) //в качесве аргумента индекс сцены которую хотим загрузить
    {
        SceneManager.LoadScene(index);
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Exit!"); //выход битч
    }
}
