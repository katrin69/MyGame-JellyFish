using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //этот скрипт для контролля камеры

    public string TerrainLayerName = "Terrain"; //Наш песок
    public string EnemyLayerName = "Enemy";

    public int TerrainLayerIndex;
    public int EnemyLayerIndex;

    public LayerMask RaycastLayerMask;

    private Camera Camera; //вообще впринципе камера
    private Transform Target; //Наш игрок
    private Vector3 offset = new Vector3(0,20,-20); //место где камера стоит
    private float smoothSpeed = 2f; //движение камеры за игроком


    private int GroundRaycastCount; 
    private RaycastHit[] RaycastResults = new RaycastHit[10]; //Масик луча

    private void Awake()
    {
        TerrainLayerIndex = LayerMask.NameToLayer(TerrainLayerName);
        EnemyLayerIndex = LayerMask.NameToLayer(EnemyLayerName);

        RaycastLayerMask = LayerMask.GetMask(TerrainLayerName, EnemyLayerName);
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

    public bool GetClickPoint(Vector3 mousePosition, out Vector3 groundPoint) //точка кликанья мышки
    {
        bool result = false; //
        groundPoint = Vector3.zero; //заполняется изначально нулём

        Ray ray = Camera.ScreenPointToRay(mousePosition); //луч через камероу
        GroundRaycastCount = Physics.RaycastNonAlloc(ray, RaycastResults, 500, RaycastLayerMask); //

        if (GroundRaycastCount != 0)
        {
            for (int i = 0; i < GroundRaycastCount; i++)
            {
                if (RaycastResults[i].transform.gameObject.layer == EnemyLayerIndex)
                {
                    groundPoint = RaycastResults[i].point;
                    return true;
                }
                else if (RaycastResults[i].transform.gameObject.layer == TerrainLayerIndex)
                {
                    groundPoint = RaycastResults[i].point;
                }
            }

            result = groundPoint != Vector3.zero;
        }

        return result; //вот это детка
    }


    public Vector3 WorldToCanvasPosition(Vector3 point)
    {
        return Camera.WorldToScreenPoint(point);
    }
}
