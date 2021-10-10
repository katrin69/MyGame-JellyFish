using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance; //������ �����

    private List<GameObject> pooledObject = new List<GameObject>(); //������ ����

    [SerializeField] private GameObject bulletPrefab;

    private Transform BulletParent; //���� ������� �����

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        GameObject bullets = new GameObject(); //����� ������� ������
        bullets.name = "Bullets_Nya"; //�������� �����
        BulletParent = bullets.transform; //����������� ������� ����� � �����
    }

    //������ ����� ˸��

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObject.Count; i++)//���� i ������ ����� ������ � ������ � ���� ������ �� ������� �� ��������� ������
                                                    // ������� ����
        {
            if (!pooledObject[i].activeInHierarchy) //����������, ������� �� ������� ������ � �����
            {
                return pooledObject[i];//��������� ������ ������
            }
        }
        GameObject gm = createObject();
        pooledObject.Add(gm);
        return gm;
    }

    public GameObject createObject()
    {
        GameObject obj = Instantiate(bulletPrefab); //������ ������
        obj.transform.SetParent(BulletParent);
        obj.SetActive(false); //����������/������������ ������� ������ � ����������� �� ��������� ��������
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