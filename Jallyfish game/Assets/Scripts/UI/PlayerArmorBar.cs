using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerArmorBar : MonoBehaviour
{
    public Image mask;
    float originalSize; 

    private void Start()
    {
        originalSize = mask.rectTransform.rect.width; 
    }

    public void SetValue(float value) 
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
