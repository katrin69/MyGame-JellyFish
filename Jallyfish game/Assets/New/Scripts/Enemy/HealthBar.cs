using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Sprite mask;
    float originalSize;

    private Camera mainCamera;

    private void Start()
    {
        //originalSize = mask.rectTransform.rect.width;
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
        //mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
