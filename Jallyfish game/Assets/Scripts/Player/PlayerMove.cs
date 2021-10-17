using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 15f; // Скорость движения   
    public float fastSpeed = 25f; // Скорость при ускореении   
    private float realSpeed; // Текущее значение скорости . Либо обычная либо увеличенная


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
    {
        realSpeed = moveSpeed;
    }


    void Update()
    {
        GroundChexk();

        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        FastSpeed(); //метод для ускорения

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
            Quaternion lookRotation = Quaternion.LookRotation(movement);//расщёт повора цели

            lookRotation.x = 0; // чтобы мы не повернули ее непо той оси
            lookRotation.z = 0;

            transform.rotation = Quaternion.Lerp(transform.rotation/*начальная точка*/,
                                 lookRotation/*куда хотим смотреть */,
                                 Time.deltaTime * rotationSpeed);

        }

        //Ходьба
        rb.MovePosition(rb.position + movement * realSpeed * Time.fixedDeltaTime); //двигаемся
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sea"))
        {
           
            print("СУША БЛЯ");

        }
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

    void FastSpeed() //метод который переключает скорость
    {
        PlayerFastSpeedBar.instance.SetValue( moveSpeed/(float)realSpeed);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            realSpeed = fastSpeed;
        }
        else
        {
            realSpeed = moveSpeed;
        }
    }
}


