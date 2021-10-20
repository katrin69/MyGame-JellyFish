using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SavePrefs : MonoBehaviour
{
    public float X, Y, Z; //позиции
    private Transform player;//получаем доступ к компоненту чтобы получить эти позиции

    private void Start()
    {
        player = GetComponent<Transform>();
        Load(); //метод загрузки
    }
    private void Update()
    {
        X = player.transform.position.x; //координаты обьекта по X
        Y = player.transform.position.y;
        Z = player.transform.position.z;
        Save(); //метод сохранения
    }

    public void Save() //метод сохранения сохраняет переменные
    {
        PlayerPrefs.SetFloat("X", X);
        PlayerPrefs.SetFloat("Y", Y);
        PlayerPrefs.SetFloat("Z", Z);
        PlayerPrefs.Save(); //сохраняет данные на диск
    }

    public void Load() //метод загрузки делает проверку
    {
        if (PlayerPrefs.HasKey("X")) //существует ли ключ с таким именем
        {
            X = PlayerPrefs.GetFloat("X"); //если да то переменная получает сохранённое з
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
