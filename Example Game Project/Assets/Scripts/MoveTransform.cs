using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransform : MonoBehaviour
{
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime;
        transform.position += transform.forward * Time.deltaTime;
    }
}
