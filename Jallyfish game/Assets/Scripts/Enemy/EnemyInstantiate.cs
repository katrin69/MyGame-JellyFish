using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiate : MonoBehaviour
{
    public GameObject theEnemy;
    public LayerMask TerrainMask;
    public float xPos;
    public float yPos = 200f;
    public float zPos;
    public float enemyCount;

    //Никита любит Лёшу

    public float distance = 200f;
    List<Ray> dabugRays = new List<Ray>();

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
       // foreach (var ray in dabugRays)
       // {
       //     Debug.DrawRay(ray.origin, ray.direction * distance);
       // }
    }

    

    IEnumerator EnemyDrop()
    {
        while (enemyCount < 10)
        {
            xPos = Random.Range(-12, 200);
            zPos = Random.Range(-12, 200);

            Vector3 pos = new Vector3(xPos, yPos, zPos);
            Ray ray = new Ray(pos, Vector3.down);
            dabugRays.Add(ray);

            if (Physics.Raycast(ray, out var hitInfo, distance, TerrainMask))
            {
                pos = hitInfo.point;
                pos.y += 5f;
                Instantiate(theEnemy, pos, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
                enemyCount++;
            }

            
        }
    }
}
