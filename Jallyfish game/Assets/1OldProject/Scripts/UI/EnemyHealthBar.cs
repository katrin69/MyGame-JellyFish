using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image mask; 
    float originalSize; 

    private Camera mainCamera; 

    private void Start()
    {
        originalSize = mask.rectTransform.rect.width; 
        mainCamera = Camera.main; 
    }

    private void Update()
    {
        if (mainCamera != null) 
        {
            transform.LookAt(mainCamera.transform);
        }
    }

    public void SetValue(float value) 
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
