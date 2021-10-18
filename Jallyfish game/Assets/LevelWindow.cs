using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelWindow : MonoBehaviour
{
    private Text levelText;
    private Levels levels; //������ �� ������ � ��������

    private void Awake()
    {
        levelText = transform.Find("LevelText").GetComponent<Text>();


    }

    //����� ����� ������
    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = "" + (levelNumber + 1); //����� ��������� � ������
    }

    //����� ��� ��������� �������
    public void SetLevelSystem(Levels levels)
    {
        this.levels = levels;

        //����� �������� ����� ������ �� �� �������� �������
        SetLevelNumber(levels.GetLevel());

        //������������� �� ���������
        levels.OnLevelChanged += Levels_OnLevelChanged;
    }

    private void Levels_OnLevelChanged(object sender, System.EventArgs e)
    {
        SetLevelNumber(levels.GetLevel());
    }

}
