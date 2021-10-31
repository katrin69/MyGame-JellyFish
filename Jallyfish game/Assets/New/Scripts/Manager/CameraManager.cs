using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //этот скрипт для контролля камеры

    private Camera Camera; //вообще впринципе камера
    private Transform Target; //Наш игрок
    private Vector3 offset = new Vector3(0,20,-15); //место где камера стоит
    private float smoothSpeed = 2f; //движение камеры за игроком

    private LayerMask GroundLayerMask; //Наш песок
    private int GroundRaycastCount; 
    private RaycastHit[] GroundRaycastResults = new RaycastHit[1]; //Масик луча

    private void Awake()
    {
        GroundLayerMask = LayerMask.GetMask("Terrain");
    }

    public void Initialize(Camera camera, Transform target) //метод создания камеры и игрока в камере
    {
        Camera = camera;
        Target = target;

        //чтобы при включении камера была ок а не появлялась хер знает от куда
        Camera.transform.position = Target.position + offset;
        Camera.transform.LookAt(Target);
    }

    private void FixedUpdate()
    {
        Vector3 desirePosition = Target.position + offset; //желаемая позиция 
        Vector3 smoothedPosition = Vector3.Lerp(Camera.transform.position, desirePosition, smoothSpeed * Time.deltaTime);
        Camera.transform.position = smoothedPosition;

        Camera.transform.LookAt(Target); //смотрит на игрока
    }

    public bool GetGroundPoint(Vector3 mousePosition, out Vector3 groundPoint) //точка кликанья мышки
    {
        bool result = false; //
        groundPoint = Vector3.zero; //заполняется изначально нулём

        Ray ray = Camera.ScreenPointToRay(mousePosition); //луч через камероу
        GroundRaycastCount = Physics.RaycastNonAlloc(ray, GroundRaycastResults, 500, GroundLayerMask); //

        if (GroundRaycastCount != 0)
        {
            groundPoint = GroundRaycastResults[0].point; //потом заполняется значением первым в массиве результатов 
            result = true;
        }

        return result; //вот это детка
    }
}
