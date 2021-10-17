using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Timer : MonoBehaviour
{
    Image timerBar; //Картинка
    public float maxTimer = 8f;
    float timeLeft;

    private void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = maxTimer;
    }
    private void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTimer;
        }
        else
        {
            Debug.Log("Ускорение закончилось");
        }
    }
}
