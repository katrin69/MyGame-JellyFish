using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelBar : MonoBehaviour
{
    public Text levelText; //показывает лэвл

    public void SetValue(float level)
    {
        levelText.text = level.ToString();
    }
}
