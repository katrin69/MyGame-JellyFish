using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMove : MonoBehaviour
{
    //скорость
    public float moveSpeed = 15f; // Скорость движения   
    public float fastSpeed = 25f; // Скорость при ускореении
    private float realSpeed; // Текущее значение скорости . Либо обычная либо увеличенная
    Vector3 movement; //напрвление перса
   
    public Camera cam;
    public GameObject JellyfishModel; // Моделька медузки
    public Rigidbody rb; // 

    [Space]
    [Space]

    //выносливость
    [SerializeField] Slider sliderStanina; //достаём наш слайдер
    [SerializeField] float currentStamina;
    [SerializeField] float maxValueStamina;
    [SerializeField] float minValueStamina;
    //  [SerializeField] float staminaReturn = 4f;
    [Space]
    [Space]


    //поворот
    public float rotationSpeed = 5f; // Скорость поворота   
    public float distToGround; //Дистанция до земли
    
    //мыш и луч
    public Ray MouseRay;               // Луч, вдоль которого мы пускаем RayCast
    public LayerMask TerrainMask;       // Фильтр по которому мы отсеиваем все, кроме песка

    private void Awake()
    {

        TerrainMask = LayerMask.GetMask("Terrain");     // Создаем фильтр по слою Terrain
    }

    private void Start()
    {
        realSpeed = moveSpeed;

        sliderStanina.maxValue = maxValueStamina;
        sliderStanina.minValue = minValueStamina;
        currentStamina = maxValueStamina;
    }


    void Update()
    {
        sliderStanina.value = currentStamina;
        GroundChexk();

        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        StaminaChecked();
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            realSpeed = fastSpeed; //когда кнопка зажата то скорость будет быстрее
            currentStamina -=  Time.deltaTime * 10f; 
        }
        else
        {
            realSpeed = moveSpeed; //когда кнопка не жата то скорость становится прежней
            currentStamina += Time.deltaTime /2f;
        }
    }

    //private void Stamina()
    //{
    //    if (maxValueStamina > 100f)
    //    {
    //        maxValueStamina = 100f; //чтобы больше 100 не поднимался
    //    }
    //    staminaSlider.value = currentStamina;
    //}

    private void StaminaChecked()
    {
        if (currentStamina <= minValueStamina)
        {
            currentStamina = minValueStamina;
        }

        if(currentStamina >= maxValueStamina)
        {
            currentStamina = maxValueStamina;
        }
    }

   
}


