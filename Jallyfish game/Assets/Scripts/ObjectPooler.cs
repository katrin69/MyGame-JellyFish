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
        // for (int i = 0; i < amountToPool; i++) //���� i ������ ��� 20 ��������� ������ � ��������� � ������
        //  {
        //     GameObject obj = Instantiate(bulletPrefab); //������ ������
        //     obj.SetActive(false); //����������/������������ ������� ������ � ����������� �� ��������� ��������
        //     pooledObject.Add(obj); // ��������� � ������
        //  }
    }
    //������ ����� ˸��

    public GameObject GetPooledObject()
    {
        Debug.Log(pooledObject.Count);
        for (int i = 0; i < pooledObject.Count; i++)//���� i ������ ����� ������ � ������ � ���� ������ �� ������� �� ��������� ������
                                                    // ������� ����
        {
            if (!pooledObject[i].activeInHierarchy) //����������, ������� �� ������� ������ � �����
            {
                return pooledObject[i];//��������� ������ ������
            }
        }
        GameObject gm = createObject();
        return gm;
    }

    public GameObject createObject()
    {
        GameObject obj = Instantiate(bulletPrefab); //������ ������
        obj.SetActive(false); //����������/������������ ������� ������ � ����������� �� ��������� ��������
        return obj;
    }
}
