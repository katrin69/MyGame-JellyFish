using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения   
    public float rotationSpeed = 5f; // Скорость поворота   


    public Rigidbody rb; // 

    public Camera cam;

    public GameObject JellyfishModel; // Моделька медузки

    Vector3 movement; //напрвление перса
    Vector3 mousePos;

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
       
        //поворот медузы на мышку
        MouseRay = cam.ScreenPointToRay(Input.mousePosition); //создаст луч который будет использовать экран камеры
        float hitDist = 0.0f; //расстояние попадание на землю
        Plane playerPlane = new Plane(Vector3.up, transform.position);

       if (playerPlane.Raycast(MouseRay, out hitDist) && Input.GetMouseButton(0))
        {
            Vector3 targetPoint = MouseRay.GetPoint(hitDist); //Возвращает точку в единицах измерения вдоль луча. hitDist расстояние попадания
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position); //расщёт повора цели
            targetRotation.x = 0; // чтобы мы не повернули ее непо той оси
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);
        }

        //поворачивался по направлению движения
        if (movement.magnitude > Mathf.Abs(0.5f))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation/*начальная точка*/, 
                                 Quaternion.LookRotation(movement)/*куда хотим смотреть */,
                                 Time.deltaTime * rotationSpeed);

        }
      
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); //двигаемся

    }

}
