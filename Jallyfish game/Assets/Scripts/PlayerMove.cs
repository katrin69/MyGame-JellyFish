using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // —корость движени€   
    public float rotationSpeed = 5f; // —корость поворота   


    public Rigidbody rb; // 

    public Camera cam;

    public GameObject JellyfishModel; // ћоделька медузки

    Vector3 movement; //напрвление перса
    Vector3 mousePos;

    public Ray MouseRay;               // Ћуч, вдоль которого мы пускаем RayCast
    public RaycastHit[] CastResults;   // –езультаты RayCast

    public LayerMask TerrainMask;       // ‘ильтр по которому мы отсеиваем все, кроме песка

    [Space(10)]
    public float VerticalOffset;        // ¬ертикальное отклонение от текущей позиции

    [Range(1, 100)]
    public float VerticalDumpening = 10;  //  оэффициент ослаблени€ вертикальных колебаний

    public Vector3 PositionOffset;      // ќбщее отклонение от текущей позиции

    public Vector3 TargetPosition;      // ѕозици€, в которую нужно прийти

    public Vector3 ZeroVelocity = Vector3.zero;  // ѕрст нол

    public float Height;    // »сходна€ высота медузки над песком

    private void Awake()
    {
        CastResults = new RaycastHit[1];       // —оздаем массив с одним результатам
        TerrainMask = LayerMask.GetMask("Terrain");     // —оздаем фильтр по слою Terrain
    }

    private void Start()
    {                       // наша позици€ - вектор вниз - результат - дальность каста - фильтр 
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 10, TerrainMask)) // ƒелаем каст под себ€
        {
            Height = (hit.point - transform.position).magnitude; // записываем рассто€ние от медузки до песка
        }

        TargetPosition = transform.position; // «аписываем текущую позицию как конечную
    }

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");

        MouseRay = cam.ScreenPointToRay(Input.mousePosition); //создаст луч который будет использовать экран камеры
        float hitDist = 0.0f; //рассто€ние попадание на землю
        Plane playerPlane = new Plane(Vector3.up, transform.position);
       

        if (playerPlane.Raycast(MouseRay, out hitDist) && Input.GetMouseButton(0))
        {
            Vector3 targetPoint = MouseRay.GetPoint(hitDist); //¬озвращает точку в единицах измерени€ вдоль луча. hitDist рассто€ние попадани€
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position); //расщЄт повора цели
            targetRotation.x = 0; // чтобы мы не повернули ее непо той оси
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);
        }

        //поворачивалс€ по направлению движени€
        transform.rotation = Quaternion.Lerp(transform.rotation/*начальна€ точка*/, Quaternion.LookRotation(movement)/*куда хотим смотреть */, Time.deltaTime * rotationSpeed);

        //if (Vector3.Angle(Vector3.forward, movement) > 1f || Vector3.Angle(Vector3.forward, movement) == 0)
        //{
        //    Vector3 direct = Vector3.RotateTowards(transform.forward, movement, moveSpeed, 0.0f);
        //    transform.rotation = Quaternion.LookRotation(direct);
        //}
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); //двигаемс€

        //Vector3 targetDirection = mousePos - transform.position; // считаем направление от себ€ то конечной точки
        //float angle = Mathf.Atan2(targetDirection.z, targetDirection.x) * Mathf.Rad2Deg-90 ;
        //transform.eulerAngles = new Vector3(transform.rotation.x,  angle, transform.rotation.z);


        //if (Input.GetMouseButton(0)) // ѕровер€ем, нажата ли лева€ кнопка мышки
        //{
        //    MouseRay = cam.ScreenPointToRay(Input.mousePosition); // Ѕерем луч из камеры относительно позиции мышки

        //    int results = Physics.RaycastNonAlloc(MouseRay, CastResults, 500, TerrainMask); // ƒелаем каст по лучу

        //    if (results > 0)
        //    {
        //        TargetPosition = CastResults[0].point + Vector3.up * Height; // ≈сли результатов >1 то в конечную позицию записываем точку, куда мышка кликнула
        //    }

        //    Vector3 targetDirection = TargetPosition - transform.position; // считаем направление от себ€ то конечной точки

        //    var angle = Mathf.Atan2(targetDirection.z, targetDirection.x) * Mathf.Rad2Deg;// находим угол
        //    transform.eulerAngles = new Vector3(transform.rotation.x, -angle, transform.rotation.z); // задаЄм угол
        //}



        //VerticalOffset = Mathf.Sin(Time.realtimeSinceStartup) / VerticalDumpening; // —читаем вертикальное отклонение по синусу от времени, получаем плавное изменение величины от -1 до 1

        //PositionOffset = JellyfishModel.transform.position; // берем позицию модели медузки
        //PositionOffset.y += VerticalOffset; // добавл€ем ей текущее вертикальное отклонение
        //JellyfishModel.transform.position = PositionOffset; // возвращаем отклонение обратно в медузку - получаем плавные колебани€ вверх-вниз




    }

}
