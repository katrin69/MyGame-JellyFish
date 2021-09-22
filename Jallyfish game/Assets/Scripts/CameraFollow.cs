using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //ссылка на то чему мы будем следовать,игрок
    public Vector3 offset; //возможность смещать камеру по трём осям

    public float smoothSpeed = 0.125f; //как быстро камера будет привязывать к цели,регулировать плавность

    private void FixedUpdate()
    {
        Vector3 desirePosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desirePosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
