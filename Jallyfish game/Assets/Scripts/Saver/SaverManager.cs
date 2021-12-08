using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json;
using System.IO;

public class SaverManager : MonoBehaviour
{
    private string SavingPath;
    private string FileName = "GameSave.json"; //название файла с сохранением

    private void Awake()
    {
        Debug.Log("Application persistent data path: " + Application.persistentDataPath);
        Debug.Log("Application data path: " + Application.dataPath);

        SavingPath = Application.dataPath + "/" + FileName;
    }

    public void Save(SaverData saverData) //метод сохранения
    {
        string data = JsonConvert.SerializeObject(saverData);

        Debug.Log("Saving");

        File.WriteAllText(SavingPath, data);
    }

    public bool IsSaveDataExists() //сохранение существующих данных
    {
        return File.Exists(SavingPath);
    }

    public SaverData Load() //з-агрузка
    {
        string data = File.ReadAllText(SavingPath);
        return JsonConvert.DeserializeObject<SaverData>(data);
    }
}