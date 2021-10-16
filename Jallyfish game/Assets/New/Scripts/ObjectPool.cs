using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private Transform ParentTransform; //папка 

    private GameObject Prefab; //обьект префаба

    private List<GameObject> AvailableObjects = new List<GameObject>(); //создаём лист с обьектами

    public ObjectPool(GameObject basicPrefab) //пул принимающий обьект 
    {
        Prefab = basicPrefab; //передаем его в конструкторе и сохраняем

        GameObject parent = new GameObject(); //папка где будут пулы чтобы не мешали
        parent.name = Prefab.name + "_Pool";
        ParentTransform = parent.transform;
    }

    public GameObject GetFromPool() //берем из басика наши обьект
    {
        if (AvailableObjects.Count == 0) //если в листе ничего нет то создаём обьект
        {
            GameObject newGameObject = GameObject.Instantiate(Prefab); //новый обьект
            newGameObject.SetActive(false); //отключаем чтобы не мешал на сцене
            newGameObject.transform.SetParent(ParentTransform); //суём в папку
            AvailableObjects.Add(newGameObject); //добавляем в лист
        }

        GameObject objectToReturn = AvailableObjects[0]; //возвращаем обьект который первый
        objectToReturn.transform.SetParent(null); // переносим в корень наш обьект
        AvailableObjects.RemoveAt(0); //удаляем первый обьет и сдвигаем лист

        return objectToReturn; //возращаем наш обьект
    }

    public void ReturnToPool(GameObject gameObject) //метод который возращает обьекты в пул
    {
        gameObject.SetActive(false); //отключаем
        gameObject.transform.SetParent(ParentTransform); //возращаем в общую папку со сцены
        AvailableObjects.Add(gameObject); //добавляем в лсит
    }
}
