using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFastSpeedBar : MonoBehaviour
{
    public static PlayerFastSpeedBar instance { get; private set; }
    public Image mask; //Картинка
    float originalSize; //исходный размер маски

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        originalSize = mask.rectTransform.rect.width; //присваем размер из значений ширины маски
    }

    public void SetValue(float value) //будем менять в моент получения урона
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
