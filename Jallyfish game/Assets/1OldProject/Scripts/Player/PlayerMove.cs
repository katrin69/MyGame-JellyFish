using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMove : MonoBehaviour
{
    //движение перса
    public float moveSpeed = 15f; 
    public float fastSpeed = 25f; // ускорение
    private float realSpeed; 
    Vector3 movement;
   
    public Camera cam;
    public GameObject JellyfishModel; 
    public Rigidbody rb; 

    [Space]
    [Space]

    //Ускорение 
    [SerializeField] Slider sliderStanina; //Бар стамина
    [SerializeField] float currentStamina; //текущее
    [SerializeField] float maxValueStamina; 
    [SerializeField] float minValueStamina; 
    //  [SerializeField] float staminaReturn = 4f;
    [Space]
    [Space]


    //скорость поворота
    public float rotationSpeed = 5f; 
    public float distToGround; 
    
    //луч до песка
    public Ray MouseRay;         
    public LayerMask TerrainMask;
    public LayerMask SeaMask;    


    private void Awake()
    {

        TerrainMask = LayerMask.GetMask("Terrain");     //Достаём песок
        SeaMask = LayerMask.GetMask("Sea");     

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
        FastSpeed(); //метод ускорения

        //Луч удара
        MouseRay = cam.ScreenPointToRay(Input.mousePosition); 
        float hitDist = 0.0f; 
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        Debug.DrawRay(transform.position, transform.forward * 30f, Color.red);

        if (playerPlane.Raycast(MouseRay, out hitDist) && Input.GetMouseButton(0))
        {
            Vector3 targetPoint = MouseRay.GetPoint(hitDist); 
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position); 
            targetRotation.x = 0; 
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);
        }

        //Плавный поворот
        if (movement.magnitude > Mathf.Abs(0.5f))
        {
            Quaternion lookRotation = Quaternion.LookRotation(movement);

            lookRotation.x = 0;
            lookRotation.z = 0;

            transform.rotation = Quaternion.Lerp(transform.rotation,
                                 lookRotation,
                                 Time.deltaTime * rotationSpeed);

        }

        //Движение
        rb.MovePosition(rb.position + movement * realSpeed * Time.fixedDeltaTime);

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

       if (Physics.Raycast(isGround, out var hitInfo, distToGround * 10f, TerrainMask)) //проверка на песок
        {

            Debug.DrawRay(isGround.origin, isGround.direction * distToGround, Color.green); //рисует луч
            var dif = (hitInfo.point.y + distToGround) - transform.position.y;
            movement = Vector3.up * dif;

        }

    }

    void FastSpeed() //метод ускорения
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            realSpeed = fastSpeed; //если нажат Шифт то скоростт становится быстрой
            currentStamina -= Time.deltaTime * 10f; // но лишь на время
            if (currentStamina <= 0)
            {
                realSpeed = moveSpeed;
            }
        }
        else
        {
            realSpeed = moveSpeed; //если шифт не нажат то скорость становится прежней
            currentStamina += Time.deltaTime / 0.6f; //скорость возращается
        }
    }

    private void StaminaChecked()
    {
        if (currentStamina <= minValueStamina)
        {
            currentStamina = minValueStamina;
        }

        if (currentStamina >= maxValueStamina)
        {
            currentStamina = maxValueStamina;
        }
    }
  
}


