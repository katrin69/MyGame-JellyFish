using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaverManager : MonoBehaviour
{
    // Будет находиться в мире и проверять нажатиена клавиши сохранения

    public UnityEvent save;
    public UnityEvent load;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6)) //сохранение на кнопку F6
        {
            save?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.F7)) //загрузка 
        {
            load?.Invoke();
        }
    }
}
