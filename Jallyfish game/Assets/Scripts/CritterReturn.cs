using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterReturn : MonoBehaviour
{
    private BasicPool objectPool;

    void Start()
    {
        objectPool = FindObjectOfType<BasicPool>();
    }
    private void OnDisable()
    {
        if (objectPool != null)
        {
            objectPool.ReturnCritter(this.gameObject);
        }
    }


}
