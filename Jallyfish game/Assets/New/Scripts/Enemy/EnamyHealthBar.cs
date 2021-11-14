using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnamyHealthBar : MonoBehaviour
{
    public Image HealthBar;

    public void SetValue(float value)
    {
        HealthBar.fillAmount = value;
    }
}
