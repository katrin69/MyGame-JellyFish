using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public GameObject JellyfishPrefab; //создаёт обьект Медузы
    public GameObject SharkPrefab;//создаёт обьект акулы
    public GameObject BulletPrefab;//создаёт обьект пули

    private Dictionary<EObjectType, ObjectPool> Pools = new Dictionary<EObjectType, ObjectPool>(); //словарь пулов в зависимости от типа . Зная тип мы можем полкучить пул для обектов

    private Dictionary<GameObject, EObjectType> InstantiatedObjects = new Dictionary<GameObject, EObjectType>(); //словарь всех обьектов который были сделаны через \рм с указанием типа


    public GameObject GetObjectInstance(EObjectType objectType) //метод через который мы получаем обьект опр типа  
    {
        GameObject objectToReturn; //обьект который будет возвращен

        if (Pools.ContainsKey(objectType) == false) //если нету пула для такого типа
        {
            CreateNewPool(objectType); //метод создаёт пул
        }

        objectToReturn = Pools[objectType].GetFromPool(); //мы этот пул просим создать новый обьект

        if (InstantiatedObjects.ContainsKey(objectToReturn) == false) //еслив этом списке нету ВОТ ЭТОГО обьекта то мы туда его туда добавляем и указываем тип ведём учёт обьектов
        {
            InstantiatedObjects.Add(objectToReturn, objectType);
        }

        return objectToReturn; //возращаем обьект
    }

    public void ReturnToPool(GameObject gameObject) //метод возращает обратно в пул
    {
        if (InstantiatedObjects.ContainsKey(gameObject)) //если в нашем словаре есть наш обект то мы берём его тип
        {
            EObjectType objectType = InstantiatedObjects[gameObject];

            ObjectPool pool = Pools[objectType]; 
            pool.ReturnToPool(gameObject); //идём в пулы и щем наш пул по этолму ьтпу 
        }
        else
        {
            Debug.Log("Kakaya to laja");
        }
    }

    private void CreateNewPool(EObjectType objectType) //метод создаёт пул
    {
        ObjectPool newPool; //новый пул

        switch (objectType) //оператор принимает названия обьекта и создаём пулы этих обьектов
        {
            case EObjectType.Jellyfish:
                newPool = new ObjectPool(JellyfishPrefab);
                break;

            case EObjectType.Shark:
                newPool = new ObjectPool(SharkPrefab);
                break;

            case EObjectType.Bullet:
                newPool = new ObjectPool(BulletPrefab);
                break;

            default: throw new ArgumentOutOfRangeException("UNKNOWN OBJECT TYPE FOR RESOURCE MANAGER");
        }

        Pools.Add(objectType, newPool); //добавляет
    }
}
