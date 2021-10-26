using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image mask; //Картинка
    float originalSize; //исходный размер маски

    private Camera mainCamera; //вытаскиваем камеру

    private void Start()
    {
        originalSize = mask.rectTransform.rect.width; //присваем размер из значений ширины маски
        mainCamera = Camera.main; //вытаскиваем камеру
    }

    private void Update()
    {
        if (mainCamera != null) //если камера НЕ пуста то бар смотрит на камеру
        {
            transform.LookAt(mainCamera.transform);
        }
    }

    public void SetValue(float value) //будем менять в моент получения урона
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
