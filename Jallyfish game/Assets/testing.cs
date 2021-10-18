using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{

    [SerializeField] private LevelWindow levelWindow;

    private void Awake()
    {
        Levels levels = new Levels();
        Debug.Log(levels.GetLevel()); //�������� ������� �������
        //��������� ����
        levels.AddExperience(50);
        Debug.Log(levels.GetLevel()); //�������� ������� �������

        levels.AddExperience(60);
        Debug.Log(levels.GetLevel()); //�������� ������� �������


        levelWindow.SetLevelSystem(levels); //��������� ��������� ������ �������
    }
}
