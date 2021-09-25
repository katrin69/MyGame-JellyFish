using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiate : MonoBehaviour
{
    public GameObject theEnemy;
    public LayerMask TerrainMask;
    public float xPos;
    public float yPos = -100f;
    public float zPos;
    public float enemyCount;

    //Никита любит Лёшу

    public float distance = 200f;

    private void Awake()
    {
        TerrainMask = LayerMask.GetMask("Terrain");
    }

    void Start()
    {
        StartCoroutine(EnemyDrop());
        // Instantiate(theEnemy, new Vector3(0, 0, 0), Quaternion.identity);
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, -Vector3.up);
        Debug.DrawRay(ray.origin, ray.direction * distance);

        if (Physics.Raycast(ray,distance, TerrainMask))
        {

        }
    }

    

    IEnumerator EnemyDrop()
    {
        while (enemyCount < 10)
        {
            xPos = Random.Range(-12, 200);
            zPos = Random.Range(-12, 200);
            Instantiate(theEnemy, new Vector3(xPos, yPos, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            enemyCount++;
        }
    }
}
