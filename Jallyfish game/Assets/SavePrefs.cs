using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SavePrefs : MonoBehaviour
{
    public float X, Y, Z; //�������
    private Transform player;//�������� ������ � ���������� ����� �������� ��� �������

    private void Start()
    {
        player = GetComponent<Transform>();
        Load(); //����� ��������
    }
    private void Update()
    {
        X = player.transform.position.x; //���������� ������� �� X
        Y = player.transform.position.y;
        Z = player.transform.position.z;
        Save(); //����� ����������
    }

    public void Save() //����� ���������� ��������� ����������
    {
        PlayerPrefs.SetFloat("X", X);
        PlayerPrefs.SetFloat("Y", Y);
        PlayerPrefs.SetFloat("Z", Z);
        PlayerPrefs.Save(); //��������� ������ �� ����
    }

    public void Load() //����� �������� ������ ��������
    {
        if (PlayerPrefs.HasKey("X")) //���������� �� ���� � ����� ������
        {
            X = PlayerPrefs.GetFloat("X"); //���� �� �� ���������� �������� ���������� �
        }
        if (PlayerPrefs.HasKey("Y"))
        {
            X = PlayerPrefs.GetFloat("Y");
        }
        if (PlayerPrefs.HasKey("Z"))
        {
            X = PlayerPrefs.GetFloat("Z");
        }
        player.transform.position = new Vector3(X, Y, Z);
    }
}
