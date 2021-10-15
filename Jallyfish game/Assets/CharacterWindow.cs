using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterWindow : MonoBehaviour
{
    //ссылки на все обьекты
    private Image healthImage;
    private Image shield0Image;
    private Image shield1Image;
    private Image shield2Image;
    private Image shield3Image;


    float originalSize; //исходный размер маски
    private void Awake()
    {
        //получаем компоненты изображения
        healthImage = transform.Find("HealthBar").Find("Bar").GetComponent<Image>();
        shield0Image = transform.Find("ArmorBar_0").Find("Bar").GetComponent<Image>();
        shield1Image = transform.Find("ArmorBar_1").Find("Bar").GetComponent<Image>();
        shield2Image = transform.Find("ArmorBar_2").Find("Bar").GetComponent<Image>();
        shield3Image = transform.Find("ArmorBar_3").Find("Bar").GetComponent<Image>();

        //shieldImage.fillAmount = .3f; //колличество отображаемого изображения
    }

}
