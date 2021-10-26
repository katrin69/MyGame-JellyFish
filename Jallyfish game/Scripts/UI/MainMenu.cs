 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //����� ����������� �� ������ ������� public
    public void OpenScene(int index) //� ������� ��������� ������ ����� ������� ����� ���������
    {
        SceneManager.LoadScene(index);
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Exit!");
    }
}
