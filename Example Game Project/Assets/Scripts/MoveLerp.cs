using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLerp : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float step;
    private float progress;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(startPosition, endPosition, progress);
        progress += step;
    }
}
