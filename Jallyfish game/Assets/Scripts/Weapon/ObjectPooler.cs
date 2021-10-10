using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance; //создаём пулер

    private List<GameObject> pooledObject = new List<GameObject>(); //создаём лист

    [SerializeField] private GameObject bulletPrefab;

    private Transform BulletParent; //берём позицию пулей

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        GameObject bullets = new GameObject(); //Новый игровой обьект
        bullets.name = "Bullets_Nya"; //название папки
        BulletParent = bullets.transform; //присваеваем позицию пулей в папку
    }

    //Никита любит Лёшу

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObject.Count; i++)//Пока i меньше числа молний в списке и если Молния не активна то возращать Молнию
                                                    // вернуть ноль
        {
            if (!pooledObject[i].activeInHierarchy) //Определяет, активен ли игровой объект в Сцене
            {
                return pooledObject[i];//возращает объект Молния
            }
        }
        GameObject gm = createObject();
        pooledObject.Add(gm);
        return gm;
    }

    public GameObject createObject()
    {
        GameObject obj = Instantiate(bulletPrefab); //создаёт Молнию
        obj.transform.SetParent(BulletParent);
        obj.SetActive(false); //Активирует/деактивирует игровой объект в зависимости от заданного значения
        return obj;

        //GenericMethod<GameObject, Transform, Transform>();
    }

    public void GenericMethod<First, Second, Third>()
    {
        GenericClass<Transform> a = new GenericClass<Transform>();
        GenericClass<GameObject> b = new GenericClass<GameObject>();
        GenericClass<Quaternion> c = new GenericClass<Quaternion>();
        GenericClass<Vector3> d = new GenericClass<Vector3>();
    }
}

public class GenericClass<ClassType>
{
    private List<ClassType> List = new List<ClassType>();
}

public class GameObjectClass
{
    private List<GameObject> List = new List<GameObject>();
}

public class TransformClass
{
    private List<Transform> List = new List<Transform>();
}

public class Vector3Class
{
    private List<Vector3> List = new List<Vector3>();
}