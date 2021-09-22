using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения   

    public Rigidbody rb; // 

    public Camera cam;

    public GameObject JellyfishModel; // Моделька медузки

    Vector3 movement;

    public Ray MouseRay;               // Луч, вдоль которого мы пускаем RayCast
    public RaycastHit[] CastResults;   // Результаты RayCast

    public LayerMask TerrainMask;       // Фильтр по которому мы отсеиваем все, кроме песка

    [Space(10)]
    public float VerticalOffset;        // Вертикальное отклонение от текущей позиции

    [Range(1, 100)]
    public float VerticalDumpening = 10;  // Коэффициент ослабления вертикальных колебаний

    public Vector3 PositionOffset;      // Общее отклонение от текущей позиции

    public Vector3 TargetPosition;      // Позиция, в которую нужно прийти

    public Vector3 ZeroVelocity = Vector3.zero;  // Прст нол

    public float Height;    // Исходная высота медузки над песком

    private void Awake()
    {
        CastResults = new RaycastHit[1];       // Создаем массив с одним результатам
        TerrainMask = LayerMask.GetMask("Terrain");     // Создаем фильтр по слою Terrain
    }

    private void Start()
    {                       // наша позиция - вектор вниз - результат - дальность каста - фильтр 
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 10, TerrainMask)) // Делаем каст под себя
        {
            Height = (hit.point - transform.position).magnitude; // записываем расстояние от медузки до песка
        }

        TargetPosition = transform.position; // Записываем текущую позицию как конечную
    }

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");

        //Движение по кругу
        if (Vector3.Angle(Vector3.forward,movement)>1f|| Vector3.Angle(Vector3.forward,movement) ==0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, movement, moveSpeed, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0)) // Проверяем, нажата ли левая кнопка мышки
        {
            MouseRay = cam.ScreenPointToRay(Input.mousePosition); // Берем луч из камеры относительно позиции мышки

            int results = Physics.RaycastNonAlloc(MouseRay, CastResults, 500, TerrainMask); // Делаем каст по лучу

            if (results > 0)
            {
                TargetPosition = CastResults[0].point + Vector3.up * Height; // Если результатов >1 то в конечную позицию записываем точку, куда мышка кликнула
            }
        }

        Vector3 targetDirection = TargetPosition - transform.position; // считаем направление от себя то конечной точки

        if (targetDirection.magnitude > 0.5f) // если расстояние больше 0.5
        {
            rb.velocity = targetDirection.normalized * moveSpeed; // то задаем объекту скорость по направлению к конечной точке
        }
        else
        {
            rb.velocity = ZeroVelocity; // если мы рядом с конечно точкой - сбрасываем скорость в 0
        }

        VerticalOffset = Mathf.Sin(Time.realtimeSinceStartup) / VerticalDumpening; // Считаем вертикальное отклонение по синусу от времени, получаем плавное изменение величины от -1 до 1

        //PositionOffset = JellyfishModel.transform.position; // берем позицию модели медузки
        //PositionOffset.y += VerticalOffset; // добавляем ей текущее вертикальное отклонение
        //JellyfishModel.transform.position = PositionOffset; // возвращаем отклонение обратно в медузку - получаем плавные колебания вверх-вниз

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        //rb.transform.LookAt(mousePos);
        //Vector3 lookDir = mousePos - rb.position;
        // transform.LookAt(transform.position + lookDir);
    }

}
