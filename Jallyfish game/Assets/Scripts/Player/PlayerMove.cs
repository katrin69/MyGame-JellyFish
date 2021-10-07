using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения   
    public float rotationSpeed = 5f; // Скорость поворота   
    public float distToGround; //Дистанция до земли


    public Rigidbody rb; // 

    public Camera cam;

    public GameObject JellyfishModel; // Моделька медузки

    Vector3 movement; //напрвление перса

    public Ray MouseRay;               // Луч, вдоль которого мы пускаем RayCast

    public LayerMask TerrainMask;       // Фильтр по которому мы отсеиваем все, кроме песка
 private void Awake()
    {

        TerrainMask = LayerMask.GetMask("Terrain");     // Создаем фильтр по слою Terrain
    }

    private void Start()
    {                       // наша позиция - вектор вниз - результат - дальность каста - фильтр 
        //if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 10, TerrainMask)) // Делаем каст под себя
        //{
        //    Height = (hit.point - transform.position).magnitude; // записываем расстояние от медузки до песка
        //}

        //TargetPosition = transform.position; // Записываем текущую позицию как конечную
    }

    void Update()
    {
        GroundChexk();

        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
  }

    private void FixedUpdate() 
    {

        //поворот медузы на мышку
        MouseRay = cam.ScreenPointToRay(Input.mousePosition); //создаст луч который будет использовать экран камеры
        float hitDist = 0.0f; //расстояние попадание на землю
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        Debug.DrawRay(transform.position, transform.forward * 30f, Color.red);

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

        //Ходьба
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); //двигаемся

    }


    void GroundChexk()
    {
        Ray isGround = new Ray(transform.position, Vector3.down);


        if (Physics.Raycast(isGround, out var hitInfo, distToGround * 10f, TerrainMask)) //луч,точка,дистанция до земли,земля
        {

            Debug.DrawRay(isGround.origin, isGround.direction * distToGround, Color.green); //показывает луч
            //создаём новую переменую Разница . Точка где луч пересекат землю y + дистанция до земли минус позиция по y
            var dif = (hitInfo.point.y + distToGround) - transform.position.y;
            //Направление перса равно вверх умножить на разницу
            movement = Vector3.up * dif;

        }

    }
}


