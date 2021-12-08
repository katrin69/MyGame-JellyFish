using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //нашал медуза
    public Vector3 offset; //смещение

    public float smoothSpeed = 0.125f; //плавный подплыв

    private void FixedUpdate()
    {
        Vector3 desirePosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desirePosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
