using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;

    private List<GameObject> pooledObject = new List<GameObject>();
    //private int amountToPool = 20;

    [SerializeField] private GameObject bulletPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    private void Start()
    {
        // for (int i = 0; i < amountToPool; i++) //Пока i меньше чем 20 создавать Молнию и добавлять в Список
        //  {
        //     GameObject obj = Instantiate(bulletPrefab); //создаёт Молнию
        //     obj.SetActive(false); //Активирует/деактивирует игровой объект в зависимости от заданного значения
        //     pooledObject.Add(obj); // Добавляет в список
        //  }
    }
    //Никита любит Лёшу

    public GameObject GetPooledObject()
    {
        Debug.Log(pooledObject.Count);
        for (int i = 0; i < pooledObject.Count; i++)//Пока i меньше числа молний в списке и если Молния не активна то возращать Молнию
                                                    // вернуть ноль
        {
            if (!pooledObject[i].activeInHierarchy) //Определяет, активен ли игровой объект в Сцене
            {
                return pooledObject[i];//возращает обькут Молния
            }
        }
        GameObject gm = createObject();
        return gm;
    }

    public GameObject createObject()
    {
        GameObject obj = Instantiate(bulletPrefab); //создаёт Молнию
        obj.SetActive(false); //Активирует/деактивирует игровой объект в зависимости от заданного значения
        return obj;
    }
}
